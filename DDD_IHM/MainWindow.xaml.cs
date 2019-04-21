using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DTO;
using agregat;
using Mindscape.WpfElements;

namespace DDD_IHM
{


    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        entretient_control entretient_Control;
        public MainWindow()
        {
            entretient_Control = new entretient_control();
            InitializeComponent();
            load_liste_box_entretient();
        }
        public void load_liste_box_entretient()
        {
            liste_box_entretients.Items.Clear();
            foreach (Entretien entretien in entretient_Control.getAllActiveEntretient())
            {
                liste_box_entretients.Items.Add(entretien.creneau.salle.name + " " +
                    entretien.creneau.heureDebut + " " +
                    entretien.creneau.HeureFin + " " +
                    Enum.GetName(typeof(Status), entretien.creneau.status));

            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in entretient_Control.GetCandidats())
            {
                this.combobox_candidat.Items.Add(item.firstname + ' ' + item.lastname);
            }
            foreach (var item in entretient_Control.GetRecruteurs())
            {
                this.combobox_participants.Items.Add(item.firstname + ' ' + item.lastname);
            }
            foreach (var item in entretient_Control.GetSalles())
            {
                this.combobox_salle.Items.Add(item.name);
            }

        }

        private void Bouton_ajouter_participants_Click(object sender, RoutedEventArgs e)
        {
            if (combobox_participants.SelectedIndex != -1 && !liste_box_particiapants.Items.Contains(combobox_participants.SelectedItem.ToString()))
            {

                liste_box_particiapants.Items.Add(combobox_participants.SelectedItem.ToString());


            }
        }

        private void Bouton_ajouter_entretient_Click(object sender, RoutedEventArgs e)
        {
            if (combobox_candidat.SelectedIndex == -1)
            {
                label_error.Content = "choisissez un candidat";
                return;
            }
            if (combobox_salle.SelectedIndex == -1)
            {
                label_error.Content = "choisissez une salle";
                return;
            }
            if (liste_box_particiapants.Items.Count == 0)
            {
                label_error.Content = "choisissez un participant";
                return;
            }
            if (date_picker.SelectedDate == null)
            {
                label_error.Content = "choisissez une date";
                return;
            }
            try
            {
                DateTime debut = Convert.ToDateTime(date_picker.SelectedDate.ToString()).AddHours(datepicker_debut.SelectedTime.Hour);
                DateTime fin = Convert.ToDateTime(date_picker.SelectedDate.ToString()).AddHours(date_picker_fin.SelectedTime.Hour);
                List<Recruiter> recruteurs = new List<Recruiter>();
                foreach (var item in liste_box_particiapants.Items)
                {
                    recruteurs.Add(entretient_Control.GetRecruteurByName(item.ToString().Split(' ')[0], item.ToString().Split(' ')[1]));
                }

                entretient_Control.CreateEntretient(recruteurs,
                                                    entretient_Control.GetCandidatByName(combobox_candidat.SelectedItem.ToString().Split(' ')[0], combobox_candidat.SelectedItem.ToString().Split(' ')[1]),
                                                    entretient_Control.getCreneau(debut, fin, combobox_salle.SelectedItem.ToString()));



            }
            catch (Exception ex)
            {
                label_error.Content = ex.Message;
            }
            load_liste_box_entretient();
        }
    }
}
