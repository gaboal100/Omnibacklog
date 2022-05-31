USE OmniBacklog;

insert into Sagas values ('The Cosmere', 0, null);
	insert into Libros values ('White Sand', 1, 3);
	insert into Libros values ('Elantris', 2, 3);
	insert into Libros values ('The Emperors Soul', 3, 3);

	insert into Sagas values ('Mistborn', 4, 3);
		insert into Sagas values ('Mistborn the Original Trilogy', 1, 4);
			insert into Libros values ('The Final Empire', 1, 5);
			insert into Libros values ('The Well of Ascension', 2, 5);
			insert into Libros values ('The Hero of Ages', 3, 5);
			insert into Libros values ('Mistborn: Secret History', 4, 5);
		insert into Sagas values ('Wax and Wayne', 5, 4);
			insert into Libros values ('The Alloy of Law', 1, 6);
			insert into Libros values ('Shadows of Self', 2, 6);
			insert into Libros values ('The Bands of Mourning', 3, 6);
			

insert into Sagas values ('Harry Potter', 0, null);
	insert into Libros values ('Harry Potter y la Piedra Filosofal', 1, 7);
	insert into Libros values ('Harry Potter y la Cámara Secreta', 2, 7);
	insert into Libros values ('Harry Potter y el Prisionero de Azkaban', 3, 7);
	insert into Libros values ('Harry Potter y el Cáliz de Fuego', 4, 7);
	insert into Libros values ('Harry Potter y la Orden del Fénix', 5, 7);
	insert into Libros values ('Harry Potter y el Misterio del Príncipe', 6, 7);
	insert into Libros values ('Harry Potter y las Reliquias de la Muerte', 7, 7);

insert into Sagas values ('Canción de Hielo y Fuego', 0, null);
	insert into Libros values ('Juego de Tronos', 1, 8);
	insert into Libros values ('Choque de Reyes', 2, 8);
	insert into Libros values ('Tormenta de Espadas', 3, 8);
	insert into Libros values ('Festín de Cuervos', 4, 8);
	insert into Libros values ('Danza de Dragones', 5, 8);
	insert into Libros values ('Vientos de Invierno', 6, 8);
	insert into Libros values ('Sueños de Primavera', 7, 8);

insert into Sagas values ('Aventuras en la playa', 0, null);


insert into Autores values ('George RR Martin');
insert into Autores values ('JK Rowling');
insert into Autores values ('Brandon Sanderson');
insert into Autores values ('Stephen King');

insert into Generos values ('Fantasía medieval');
insert into Generos values ('Fantasía baja');
insert into Generos values ('Ciencia ficción');
insert into Generos values ('Magia');
insert into Generos values ('Terror');
insert into Generos values ('Suspense');
insert into Generos values ('Cosa');


Select * from Sagas;
Select * from Libros;

select * from AutorLibros;
select * from GenerosLibros;

select * from Generos;
select * from Autores;

select * from Usuarios;

select * from BibliotecasPersonales;

insert into AutorLibros values (4, 1);
insert into AutorLibros values (2, 1);
insert into AutorLibros values (4, 2);
insert into AutorLibros values (4, 3);
insert into AutorLibros values (4, 4);
insert into AutorLibros values (4, 5);
insert into AutorLibros values (4, 6);
insert into AutorLibros values (4, 7);
insert into AutorLibros values (4, 8);

insert into GenerosLibros values (2, 1);
insert into GenerosLibros values (2, 2);
insert into GenerosLibros values (2, 4);
insert into GenerosLibros values (3, 1);
insert into GenerosLibros values (3, 2);
insert into GenerosLibros values (3, 4);