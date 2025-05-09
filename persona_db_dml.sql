
-- Insertar personas
INSERT INTO persona (cc, nombre, apellido, genero, edad) VALUES
(1, 'Ana', 'Gómez', 'F', 28),
(2, 'Carlos', 'Martínez', 'M', 35),
(3, 'Lucía', 'Pérez', 'F', 22);

-- Insertar profesiones
INSERT INTO profesion (nom, des) VALUES
('Ingeniería de Sistemas', 'Desarrolla software y sistemas computacionales'),
('Medicina', 'Atiende la salud humana'),
('Arquitectura', 'Diseña estructuras y espacios');

-- Insertar estudios (asociaciones entre personas y profesiones)
INSERT INTO estudios (cc_per, id_prof, fecha, univer) VALUES
(1, 1, '2018-06-15', 'Universidad Nacional'),
(2, 2, '2010-09-01', 'Universidad de los Andes'),
(3, 3, '2021-02-20', 'Pontificia Universidad Javeriana');

-- Insertar teléfonos
INSERT INTO telefono (num, oper, duenio) VALUES
('3001234567', 'Claro', 1),
('3112345678', 'Movistar', 2),
('3203456789', 'Tigo', 3);
