using System;
using INSOLMIMARC.Academy.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INSOLMIMARC.Academy.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();
            // Look for any Parents.
            if (context.Tutors.Any())
            {
                return;   // DB has been seeded
            }

            var tutors = new Tutor[]
            {
                new Tutor{FirstMidName="Carson",LastName="Moreno",Type="Padre",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Tutor{FirstMidName="Meredith",LastName="Alonso",Type="Madre",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Tutor{FirstMidName="Arturo",LastName="Gómez",Type="Tutor",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Tutor{FirstMidName="Carlos",LastName="Fernández",Type="Padre",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Tutor{FirstMidName="Yan",LastName="Li",Type="Madre",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Tutor{FirstMidName="Peggy",LastName="Pérez",Type="Madre",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Tutor{FirstMidName="Laura",LastName="García",Type="Madre",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Tutor{FirstMidName="Nino",LastName="Sánchez",Type="Tutor",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };
            foreach (Tutor s in tutors)
            {
                context.Tutors.Add(s);
            }
            context.SaveChanges();


            var teachers = new Teacher[]
            {
                new Teacher{FirstMidName="Geo",LastName="Inca",Dni="12244134D",Email="geo.inca@gmail.com",EnrollmentDate =DateTime.Parse("2005-09-01"),Job="Profesor de Guitarra, Guitarra Eléctrica, Ukelele y Lenguaje Musical.",Description="Mi labor docente supera los 13 años, siempre tratando de buscar la forma de aprender a tocar el instrumento de una manera correcta sin dejar de disfrutar de ese proceso buscando la motivación en el alumno."},
                new Teacher{FirstMidName="Rick",LastName="Hunter",Dni="12244134D",Email="hunterec.rick@gmail.com",EnrollmentDate=DateTime.Parse("2005-09-01"),Job="Profesor de Clarinete, Flauta Travesera y Saxofón.",Description="Titulado Profesional en Clarinete y Maestro en Educación Primaria. Cuento con experiencia en clases individuales y colectivas para la aplicación y desarrollo de técnicas de enseñanza dinámicas, donde el aprendizaje que se lleva a cabo es lúdico."},
                new Teacher{FirstMidName="Davi",LastName="Pino",Dni="12244134D",Email="geovannyinca@gmail.com",EnrollmentDate=DateTime.Parse("2005-09-01"),Job="Profesor de Clarinete, Flauta Travesera y Saxofón.",Description="Desde siempre mi sueño ha sido ser profesor de música y hacer que mis alumnos, niños y mayores de diferentes edades, disfruten de la música tanto como yo lo hago."}

            };
            foreach (Teacher t in teachers)
            {
                context.Teachers.Add(t);
            }

            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{ID=1050,Title="Iniciación de la Música",Credits=3,Description="Este es el curso de Iniciación a la Música pensado para aquellas personas que comienzan \"desde cero\" en el lenguaje musical. Porque sin tener ninguna noción previa, comenzando por lo más básico, se acabará el curso sabiendo leer música y conociendo las principales normas teóricas.Ni siquiera es necesario poder seguir el ritmo de una canción con el pie, porque te ensañamos a hacerlo con ejemplos y ejercicios."},
                new Course{ID=4022,Title="Piano",Description="Nuestra intención con este es curso complementar y mejorar los contenidos que hay en la red para aprender a tocar el piano para principiantes. Cuánto más cursos te hagas mejor, así que te invitamos a practicar con nosotros y así aportar nuestro granito de arena a tu formación en el piano",Credits=3},
                new Course{ID=4041,Title="Fagot",Description="Para los alumnos más pequeños, uno de sus problemas más frecuentes es que tienen dificultad para tocar el fagot debido a que sus manos son aún demasiado pequeñas y sus dedos no llegan a las llaves. Para solventarlo, el mercado ofrece el fagotino y el fagot manos pequeñas",Credits=3},
                new Course{ID=1045,Title="Viola",Description="El acercamiento al instrumento y las indicaciones elementales que requiere su manejo constituyen dos aspectos básicos de esta Iniciación a la Viola, tratando por una parte de promover la enseñanza desde la misma viola (abandonando el sistema tradicional de comenzar con el violín)",Credits=4},
                new Course{ID=3141,Title="Lenguaje Musical",Description="En el curso se estudiarán los conceptos básicos que componen el lenguaje musical, clave para poder entender y llevar a cabo la interpretación de cualquier tipo de obra, sea instrumental o vocal. El curso es eminentemente práctico.",Credits=4},
                new Course{ID=2021,Title="Canto",Description="Los tres ejes fundamentales del curso son el conocimiento de la propia voz a través del trabajo de técnica vocal, la interpretación a través de ejercicios de expresión corporal y vocal y por último la pérdida del miedo escénico.",Credits=3},
                new Course{ID=2042,Title="Coro",Description="En una primera parte, con una duración estimada de 15 minutos, educarán y entrenarán sus voces. En la segunda aprenderán y practicarán el repertorio coral. Esta parte es la que más suele divertir a los peques y tendrá una duración de unos 45 minutos. En ese tiempo aprenderán los diferentes tipos de respiración, emisión, dicción vocal, lectura y como seguir las indicaciones de un director",Credits=4}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();
               
            var students = new Student[]
            {
                new Student{TutorID= tutors.Single( i => i.LastName == "Moreno").ID,FirstMidName="Carson jr",LastName="Moreno",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{TutorID= tutors.Single( i => i.LastName == "Alonso").ID,FirstMidName="Mere",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{TutorID= tutors.Single( i => i.LastName == "Gómez").ID,FirstMidName="Arturo jr",LastName="Gómez",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{TutorID= tutors.Single( i => i.LastName == "Fernández").ID,FirstMidName="Gytis",LastName="Fernández",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{TutorID= tutors.Single( i => i.LastName == "Li").ID,FirstMidName="Yan jr",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{TutorID= tutors.Single( i => i.LastName == "Pérez").ID,FirstMidName="Peggy jr",LastName="Pérez",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{TutorID= tutors.Single( i => i.LastName == "García").ID,FirstMidName="Laura jr",LastName="García",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{TutorID= tutors.Single( i => i.LastName == "Sánchez").ID,FirstMidName="Nino jr",LastName="Sánchez",EnrollmentDate=DateTime.Parse("2005-09-01")},
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();


            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.SB},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.B},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.N},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.N},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.MD},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.SF},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.SB},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();



        }
    }
}
