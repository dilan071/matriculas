namespace Matriculas
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    // Clase para representar a un estudiante
    public class Estudiante
    {
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public List<Curso> CursosMatriculados { get; set; }

        public Estudiante(string nombre, string correoElectronico)
        {
            Nombre = nombre;
            CorreoElectronico = correoElectronico;
            CursosMatriculados = new List<Curso>();
        }
    }

    // Clase para representar a un profesor
    public class Profesor
    {
        public string Nombre { get; set; }
        public List<Curso> CursosDictados { get; set; }

        public Profesor(string nombre)
        {
            Nombre = nombre;
            CursosDictados = new List<Curso>();
        }

        public void GenerarListaEstudiantes(Curso curso, string ordenamiento)
        {
            List<Estudiante> estudiantes = curso.Estudiantes;
            if (ordenamiento == "nombre")
            {
                estudiantes.Sort((e1, e2) => e1.Nombre.CompareTo(e2.Nombre));
            }
            else if (ordenamiento == "correo")
            {
                estudiantes.Sort((e1, e2) => e1.CorreoElectronico.Length.CompareTo(e2.CorreoElectronico.Length));
            }
            string nombreArchivo = curso.Nombre + ".txt";
            using (StreamWriter writer = new StreamWriter(nombreArchivo))
            {
                foreach (Estudiante estudiante in estudiantes)
                {
                    writer.WriteLine(estudiante.Nombre + " - " + estudiante.CorreoElectronico);
                }
            }
        }
    }

    // Clase para representar a un curso
    public class Curso
    {
        public string Nombre { get; set; }
        public Profesor ProfesorAsociado { get; set; }
        public List<Estudiante> Estudiantes { get; set; }

        public Curso(string nombre, Profesor profesorAsociado)
        {
            Nombre = nombre;
            ProfesorAsociado = profesorAsociado;
            Estudiantes = new List<Estudiante>();
        }

        public void AgregarEstudiante(Estudiante estudiante)
        {
            if (Estudiantes.Contains(estudiante))
            {
                throw new InvalidOperationException("El estudiante ya está matriculado en este curso");
            }
            Estudiantes.Add(estudiante);
            estudiante.CursosMatriculados.Add(this);
        }
    }

    // Clase para representar al administrador
    public class Administrador
    {
        public List<Estudiante> Estudiantes { get; set; }
        public List<Profesor> Profesores { get; set; }
        public List<Curso> Cursos { get; set; }

        public Administrador()
        {
            Estudiantes = new List<Estudiante>();
            Profesores = new List<Profesor>();
            Cursos = new List<Curso>();
        }

        public void CrearEstudiante(string nombre, string correoElectronico)
        {
            Estudiante estudiante = new Estudiante(nombre, correoElectronico);
            Estudiantes.Add(estudiante);
        }

        public void EditarEstudiante(Estudiante estudiante, string nuevoNombre, string nuevoCorreoElectronico)
        {
            estudiante.Nombre = nuevoNombre;
            estudiante.CorreoElectronico = nuevoCorreoElectronico;
        }

        public void EliminarEstudiante(Estudiante estudiante)
        {
            Estudiantes.Remove(estudiante);
        }
        public void EditarProfesor(Profesor profesor, string nuevoNombre)
        {
            profesor.Nombre = nuevoNombre;
        }

        public void EliminarProfesor(Profesor profesor)
        {
            Profesores.Remove(profesor);
        }

        public void CrearCurso(string nombre, Profesor profesorAsociado)
        {
            Curso curso = new Curso(nombre, profesorAsociado);
            Cursos.Add(curso);
            profesorAsociado.CursosDictados.Add(curso);
        }

        public void EditarCurso(Curso curso, string nuevoNombre, Profesor nuevoProfesorAsociado)
        {
            curso.Nombre = nuevoNombre;
            curso.ProfesorAsociado = nuevoProfesorAsociado;
            if (!nuevoProfesorAsociado.CursosDictados.Contains(curso))
            {
                nuevoProfesorAsociado.CursosDictados.Add(curso);
            }
        }

        public void EliminarCurso(Curso curso)
        {
            Cursos.Remove(curso);
            curso.ProfesorAsociado.CursosDictados.Remove(curso);
        }

