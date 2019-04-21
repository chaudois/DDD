 --
-- Fichier généré par SQLiteStudio v3.1.1 sur dim. avr. 21 20:46:56 2019
--
-- Encodage texte utilisé : System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table : Candidat
DROP TABLE IF EXISTS Candidat;

CREATE TABLE Candidat (
    id     INTEGER PRIMARY KEY AUTOINCREMENT
                   NOT NULL,
    nom    STRING  NOT NULL,
    prenom         NOT NULL,
    url_cv STRING
);

INSERT INTO Candidat (
                         id,
                         nom,
                         prenom,
                         url_cv
                     )
                     VALUES (
                         1,
                         'Alexandre',
                         'MEYER',
                         'D:\User\d.chaudois\CV3'
                     );

INSERT INTO Candidat (
                         id,
                         nom,
                         prenom,
                         url_cv
                     )
                     VALUES (
                         2,
                         'Antoine',
                         'SAUVINET',
                         'D:\User\d.chaudois\CV1'
                     );

INSERT INTO Candidat (
                         id,
                         nom,
                         prenom,
                         url_cv
                     )
                     VALUES (
                         3,
                         'Julien',
                         'RUBIANO',
                         'D:\User\d.chaudois\CV2'
                     );


-- Table : Creneau
DROP TABLE IF EXISTS Creneau;

CREATE TABLE Creneau (
    id        INTEGER  PRIMARY KEY AUTOINCREMENT,
    debut     DATETIME NOT NULL,
    fin       DATETIME NOT NULL,
    salle_id  INTEGER  REFERENCES Salle (id),
    status_id INTEGER  REFERENCES Status (id) 
);

INSERT INTO Creneau (
                        id,
                        debut,
                        fin,
                        salle_id,
                        status_id
                    )
                    VALUES (
                        1,
                        '19-04-2019-14:00',
                        '19-04-2019-16:00',
                        1,
                        1
                    );

INSERT INTO Creneau (
                        id,
                        debut,
                        fin,
                        salle_id,
                        status_id
                    )
                    VALUES (
                        2,
                        '19-04-2019-17:00',
                        '19-04-2019-18:00',
                        1,
                        1
                    );

INSERT INTO Creneau (
                        id,
                        debut,
                        fin,
                        salle_id,
                        status_id
                    )
                    VALUES (
                        3,
                        '19-04-2019-17:00',
                        '19-04-2019-18:00',
                        2,
                        1
                    );

INSERT INTO Creneau (
                        id,
                        debut,
                        fin,
                        salle_id,
                        status_id
                    )
                    VALUES (
                        4,
                        '19-04-2019-17:00',
                        '19-04-2019-18:00',
                        3,
                        2
                    );

INSERT INTO Creneau (
                        id,
                        debut,
                        fin,
                        salle_id,
                        status_id
                    )
                    VALUES (
                        5,
                        '19-04-2019-12:00',
                        '19-04-2019-14:00',
                        3,
                        3
                    );

INSERT INTO Creneau (
                        id,
                        debut,
                        fin,
                        salle_id,
                        status_id
                    )
                    VALUES (
                        6,
                        '19-04-2019-14:00',
                        '19-04-2019-16:00',
                        3,
                        1
                    );


-- Table : Entretient
DROP TABLE IF EXISTS Entretient;

CREATE TABLE Entretient (
    id          INTEGER PRIMARY KEY AUTOINCREMENT,
    candidat_id INTEGER REFERENCES Candidat (id) 
                        NOT NULL,
    message     STRING,
    creneau_id          REFERENCES Creneau (id) 
);

INSERT INTO Entretient (
                           id,
                           candidat_id,
                           message,
                           creneau_id
                       )
                       VALUES (
                           1,
                           1,
                           'candidat prometeur',
                           1
                       );


-- Table : entretient_recruteur
DROP TABLE IF EXISTS entretient_recruteur;

CREATE TABLE entretient_recruteur (
    entretient_id  REFERENCES Entretient (id),
    recruteur_id   REFERENCES Recruteur (id) 
);

INSERT INTO entretient_recruteur (
                                     entretient_id,
                                     recruteur_id
                                 )
                                 VALUES (
                                     1,
                                     1
                                 );

INSERT INTO entretient_recruteur (
                                     entretient_id,
                                     recruteur_id
                                 )
                                 VALUES (
                                     1,
                                     2
                                 );


-- Table : Recruteur
DROP TABLE IF EXISTS Recruteur;

CREATE TABLE Recruteur (
    id          INTEGER PRIMARY KEY AUTOINCREMENT
                        UNIQUE,
    nom         STRING  NOT NULL,
    prenom      STRING  NOT NULL,
    poste       STRING  NOT NULL,
    departement STRING  NOT NULL
);

INSERT INTO Recruteur (
                          id,
                          nom,
                          prenom,
                          poste,
                          departement
                      )
                      VALUES (
                          1,
                          'chaudois',
                          'damien',
                          'tech-lead',
                          'DSI'
                      );

INSERT INTO Recruteur (
                          id,
                          nom,
                          prenom,
                          poste,
                          departement
                      )
                      VALUES (
                          2,
                          'bignon',
                          'romain',
                          'CEO',
                          'ADMIN'
                      );


-- Table : Salle
DROP TABLE IF EXISTS Salle;

CREATE TABLE Salle (
    id  INTEGER PRIMARY KEY AUTOINCREMENT,
    nom STRING  NOT NULL
);

INSERT INTO Salle (
                      id,
                      nom
                  )
                  VALUES (
                      1,
                      'B07'
                  );

INSERT INTO Salle (
                      id,
                      nom
                  )
                  VALUES (
                      2,
                      'A01'
                  );

INSERT INTO Salle (
                      id,
                      nom
                  )
                  VALUES (
                      3,
                      'A06'
                  );

INSERT INTO Salle (
                      id,
                      nom
                  )
                  VALUES (
                      4,
                      'C05'
                  );


-- Table : Status
DROP TABLE IF EXISTS Status;

CREATE TABLE Status (
    id  INTEGER PRIMARY KEY AUTOINCREMENT
                NOT NULL,
    nom STRING  NOT NULL
);

INSERT INTO Status (
                       id,
                       nom
                   )
                   VALUES (
                       1,
                       'Reserved'
                   );

INSERT INTO Status (
                       id,
                       nom
                   )
                   VALUES (
                       2,
                       'InProgress'
                   );

INSERT INTO Status (
                       id,
                       nom
                   )
                   VALUES (
                       3,
                       'Done'
                   );

INSERT INTO Status (
                       id,
                       nom
                   )
                   VALUES (
                       4,
                       'canceled'
                   );


COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
