using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using DTO;
using System.IO;
using repository.Properties;

namespace repository
{
    public class SQLManager
    {
        const string DB_NAME = "entretient.db";
        SQLiteConnection m_dbConnection;
        public SQLManager()
        {
            m_dbConnection = new SQLiteConnection("Data Source=" + DB_NAME + "; Version=3;");
            m_dbConnection.Open();
            using (StreamReader streamReader = new StreamReader(Resources.backup))
            {
                using (SQLiteCommand command = new SQLiteCommand(streamReader.ReadToEnd(), m_dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }

        }

        public Entretien CreateEntretient(List<Recruiter> recruiters, Candidate candidate, Creneau creneau, string message = null)
        {
            using (SQLiteCommand command = new SQLiteCommand(@"INSERT INTO Entretient (
                                                                    candidat_id,
                                                                    message,
                                                                    creneau_id                                                                )
                                                                VALUES(
                                                                    '" + candidate.id + @"',
                                                                    '" + message + @"',
                                                                    '" + creneau.id + @"');", m_dbConnection))
            {
                command.ExecuteNonQuery();
            }
            Entretien newEntretien = null;
            using (SQLiteCommand command = new SQLiteCommand("select id,message,candidat_id,creneau_id from entretient order by id desc", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        reader.Read();

                        newEntretien = new Entretien(reader.GetInt32(0),
                            new List<Recruiter>(),
                            new Candidate(reader.GetInt32(2), null, null, null),
                            new Creneau(int.Parse(reader.GetString(3)), new DateTime(), DateTime.Now, null,
                            0),
                            reader.GetString(1));
                    }
                }
            }
            newEntretien.candidate = GetCandidat(newEntretien.candidate.id);
            newEntretien.creneau = getCreneau(newEntretien.creneau.id);
            foreach (var item in recruiters)
            {

                using (SQLiteCommand command = new SQLiteCommand(@"INSERT INTO entretient_recruteur (
                                                                    entretient_id,
                                                                    recruteur_id)
                                                                VALUES(
                                                                    " + newEntretien.id + @",
                                                                    " + item.id + @");", m_dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            using (SQLiteCommand command = new SQLiteCommand("select * from entretient_recruteur where entretient_id=" + newEntretien.id, m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            newEntretien.recruiter.Add(new Recruiter(reader.GetInt32(1), null, null, null, null));
                        }
                    }
                }
            }
            for (int i = 0; i < newEntretien.recruiter.Count; i++)
            {
                newEntretien.recruiter[i] = GetRecruteur(newEntretien.recruiter[i].id);
            }
            return newEntretien;
        }

        public Candidate GetCandidatByName(string lastname, string firstname)
        {

            using (SQLiteCommand command = new SQLiteCommand(@"SELECT id,
                                                                nom,
                                                                prenom,
                                                                url_cv
                                                            FROM Candidat
                                                            where nom like '" + firstname +
                                                                       "' or prenom like '" + lastname + "'; ", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {


                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new Candidate(reader.GetInt32(0),
                            reader.GetString(2),
                            reader.GetString(1),
                            new Uri(reader.GetString(3)));
                    }
                }
            }
            return null;
        }

        public Recruiter GetRecruteurByName(string firstname, string lastname)
        {
            using (SQLiteCommand command = new SQLiteCommand(@"SELECT id,
                                                                nom,
                                                                prenom,
                                                                poste,
                                                                departement
                                                            FROM Recruteur
                                                            where prenom like '" + firstname +
                                                            "' or nom like '" + lastname + "'; ", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new Recruiter(reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4));
                    }
                }
            }
            return null;
        }

        private Recruiter GetRecruteur(int id)
        {
            using (SQLiteCommand command = new SQLiteCommand(@"SELECT 
                                                                nom,
                                                                prenom,
                                                                poste,
                                                                departement
                                                            FROM Recruteur
                                                            where id=" + id + "; ", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new Recruiter(id,
                            reader.GetString(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3));
                    }
                }
            }
            return null;
        }

        private Creneau getCreneau(int id)
        {
            using (SQLiteCommand command = new SQLiteCommand(@"SELECT id,
                                                                   debut,
                                                                   fin,
                                                                   salle_id,
                                                                   status_id
                                                              FROM Creneau
                                                              WHERE id=" + id + ";", m_dbConnection))
            {

                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new Creneau(reader.GetInt32(0),
                                            Convert.ToDateTime(reader.GetString(1)),
                                            Convert.ToDateTime(reader.GetString(2)),
                                            GetSalle(reader.GetInt32(3)),
                                            reader.GetInt32(4));
                    }
                }
            }
            return null;
        }

        private Salle GetSalle(int id)
        {
            using (SQLiteCommand command = new SQLiteCommand(@"SELECT id,
                                                                   nom
                                                              FROM Salle
                                                              WHERE id=" + id + ";", m_dbConnection))
            {

                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new Salle(reader.GetInt32(0),
                                        reader.GetString(1));
                    }
                }
            }
            return null;
        }

        public IEnumerable<Creneau> getAllActiveCreaneau()
        {
            string request = @"select c.id,c.debut,c.fin,salle.id as salle_id,salle.nom,s.id as status
                            from creneau c 
                            join status s 
                            , salle 
                            on salle.id=c.salle_id
                            and s.id=status_id
                            where  (s.nom='Reserved' or s.nom = 'InProgress')
                            order by c.id;";
            using (SQLiteCommand command = new SQLiteCommand(request, m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Creneau(int.Parse(reader[0].ToString()),
                                Convert.ToDateTime(reader.GetString(1)),
                                Convert.ToDateTime(reader.GetString(2)),
                                new Salle(reader.GetInt32(3), reader.GetString(4)),
                                reader.GetInt32(5));
                        }
                    }
                }
            }

        }

        public Salle getSalleFromName(string salleName)
        {
            using (SQLiteCommand command = new SQLiteCommand(@"SELECT id,
                                                                   nom
                                                              FROM Salle
                                                              WHERE nom='" + salleName + "';", m_dbConnection))
            {

                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new Salle(reader.GetInt32(0),
                                        reader.GetString(1));
                    }
                }
            }
            return null;
        }

        public Creneau SaveCreneau(DateTime debut, DateTime fin, Salle salle)
        {
            using (SQLiteCommand command = new SQLiteCommand(@"INSERT INTO Creneau (
                                                                    debut,
                                                                    fin,
                                                                    salle_id,
                                                                    status_id
                                                                )
                                                                VALUES(
                                                                    '" + debut.ToString() + @"',
                                                                    '" + fin.ToString() + @"',
                                                                    '" + salle.id + @"',
                                                                    (select id from status where nom like 'Reserved')
                                                                );", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                }
            }
            using (SQLiteCommand command = new SQLiteCommand(@"SELECT c.id,
                                                                   debut,
                                                                   fin,
                                                                   salle_id,
                                                                   s.nom,
                                                                   status_id
                                                              FROM Creneau c
                                                              join Salle s
                                                              on c.salle_id = s.id
                                                              order by c.id desc", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return new Creneau(int.Parse(reader[0].ToString()),
                                Convert.ToDateTime(reader.GetString(1)),
                                Convert.ToDateTime(reader.GetString(2)),
                                new Salle(reader.GetInt32(3), reader.GetString(4)),
                                reader.GetInt32(5));
                        }
                    }
                }
            }
            return null;
        }

        public Candidate GetCandidat(int id)
        {
            using (SQLiteCommand command = new SQLiteCommand("select * from Candidat where id = " + id.ToString(), m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new Candidate(int.Parse(reader[0].ToString()), reader[2].ToString(), reader[1].ToString(), new Uri(reader[3].ToString()));
                    }
                }
            }
            return null;
        }

        public IEnumerable<Salle> getAllRooms()
        {
            using (SQLiteCommand command = new SQLiteCommand("select * from Salle", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return (new Salle(int.Parse(reader[0].ToString()),
                                                    reader[1].ToString()));
                        }
                    }
                }
            }
        }

        public IEnumerable<Entretien> GetEntretients()
        {
            List<Entretien> retour = new List<Entretien>();
            using (SQLiteCommand command = new SQLiteCommand("select id from entretient", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            retour.Add(new Entretien(reader.GetInt32(0), null, null, null, null));
                        }
                    }
                }
            }
            for (int i = 0; i < retour.Count; i++)
            {
                retour[i] = GetEntretient(retour[i].id);
            }
            return retour;
        }

        private Entretien GetEntretient(int id)

        {
            Entretien retour = null;
            using (SQLiteCommand command = new SQLiteCommand(@"SELECT 
                                                                candidat_id,
                                                                message,
                                                                creneau_id
                                                            FROM entretient
                                                            where id=" + id + "; ", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        reader.Read();
                        try
                        {

                            retour = new Entretien(id,
                                null,
                                new Candidate(reader.GetInt32(0), null, null, null),
                                new Creneau(reader.GetInt32(2), new DateTime(), DateTime.Now, null, 0),
                                reader.GetString(1));
                        }
                        catch (InvalidCastException)
                        {
                            retour = new Entretien(id,
                                                null,
                                                new Candidate(reader.GetInt32(0), null, null, null),
                                                new Creneau(int.Parse(reader.GetString(2)), new DateTime(), DateTime.Now, null, 0),
                                                reader.GetString(1));
                        }
                    }
                }
            }
            retour.recruiter = GetRecruteurForEntretient(id).ToList();
            retour.creneau = getCreneau(retour.creneau.id);
            retour.candidate = GetCandidat(retour.candidate.id);
            return retour;
        }

        private IEnumerable<Recruiter> GetRecruteurForEntretient(int id)
        {
            using (SQLiteCommand command = new SQLiteCommand(@"SELECT 
                                                                recruteur_id
                                                            FROM entretient_recruteur
                                                            WHERE entretient_id=" + id + "; ", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        reader.Read();
                        yield return GetRecruteur(reader.GetInt32(0));
                    }
                }
            }
        }

        public IEnumerable<Candidate> GetCandidats()
        {
            using (SQLiteCommand command = new SQLiteCommand("select * from Candidat", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return (new Candidate(int.Parse(reader[0].ToString()),
                                                    reader[1].ToString(),
                                                    reader[2].ToString(),
                                                    new Uri(reader[3].ToString())));
                        }
                    }
                }
            }
        }

        public IEnumerable<Salle> GetSalles()
        {
            using (SQLiteCommand command = new SQLiteCommand("select * from Salle", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    List<Salle> result = new List<Salle>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(new Salle(int.Parse(reader[0].ToString()), reader[1].ToString()));
                        }
                    }
                    return result;
                }
            }
        }

        public IEnumerable<Recruiter> GetRecruteurs()
        {
            using (SQLiteCommand command = new SQLiteCommand(@"SELECT id,
                                                               nom,
                                                               prenom,
                                                               poste,
                                                               departement
                                                               FROM Recruteur;", m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    List<Recruiter> result = new List<Recruiter>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Recruiter(int.Parse(reader[0].ToString()),
                                reader[1].ToString(),
                                reader[2].ToString(),
                                reader[3].ToString(),
                                reader[4].ToString()
                                );
                        }
                    }

                }
            }

        }
    }
}
