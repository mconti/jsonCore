using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace PrimaCore.Controllers
{
    public class Studente
    {
        public string Id{get;set;}
        public string Nome{get;set;}
        public string Cognome{get;set;}

        public Studente(){}
        public Studente(string riga)
        {
            if(!string.IsNullOrEmpty(riga))
            {
                string[] colonne = riga.Split(';');
                Id = colonne[0];
                Nome = colonne[1];
                Cognome = colonne[2];
            }
        }
    }

    public class Studenti : List<Studente>
    {
        //path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FileName);
        private string _fileName;

        public string FileName
        { 
            get
            {
                if( string.IsNullOrEmpty(_fileName) )
                    _fileName = "in.csv";

                return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _fileName );
            }  
            set { _fileName = value; } 
        } 
        public Studenti(){}

        public Studenti(string fileName)
        {
            FileName = fileName;

            if( File.Exists( FileName ) )
            {
                StreamReader fin = new StreamReader( FileName);
                fin.ReadLine();
                while(!fin.EndOfStream)
                {
                    string riga = fin.ReadLine();
                    this.Add(new Studente(riga));
                }
                fin.Close();
            }
        }

        public void Save()
        {

            StreamWriter fout = new StreamWriter( FileName );
            fout.WriteLine("Id;Nome;Cognome");
            
            foreach( Studente s in this )
                fout.WriteLine($"{s.Id};{s.Nome};{s.Cognome}");
            
            fout.Close();
        }

        public string NextId()
        {
            return (this.Max( s => Convert.ToInt32(s.Id) ) + 1).ToString();
        }

        public void Aggiorna( Studente newValue )
        {
            if( newValue != null )
            {
                Studente oldValue = this.Find( b => b.Id == newValue.Id );
                if( oldValue != null )
                {
                    oldValue.Nome = newValue.Nome;
                    oldValue.Cognome = newValue.Cognome;
                    Save();
                }
            }
        }

        public void Aggiungi( Studente newValue )
        {
            if( newValue != null )
            {
                // l'id che ci arriva dal chiamante non lo prendiamo per buono
                // l'id per noi è un autoincrement in modo che possa
                // fungere da chiave primaria
                newValue.Id = NextId();
                Studente oldValue = this.Find( b => b.Id == newValue.Id );
                if( oldValue == null )
                {
                    Add( newValue );
                    Save();
                }
            }
        }
        public void Cancella( int id )
        {
            Studente oldValue = this.Find( b => Convert.ToInt32(b.Id) == id );
            if( oldValue != null )
            {
                Remove( oldValue );
                Save();
            }
        }

    }
}