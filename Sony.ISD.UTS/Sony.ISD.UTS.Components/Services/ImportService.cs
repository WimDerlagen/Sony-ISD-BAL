using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.UTS.Components
{
    public class ImportService : ImportServiceBase
    {
        public ArrayList ImportUTSExport(string filePath)
        {
            this.ImportFilePath = filePath;// "~/Import/export.csv";
            this.Delimiter = ';';
            this.HasHeaders = true;

            return ImportCSVDocument();
        }

        protected override object PopulateImportObject(System.Data.IDataReader reader)
        {
            UTSImport im = new UTSImport();

            im.Vestiging = (string)reader["vestiging"];
            im.VestigingID = GetIntFromIDataReader(reader, "vestiging id");
            im.Hoofdcategorie = (string)reader["hoofdcategorie"];
            im.Subcategorie = (string)reader["subcategorie"];
            im.ArtikelID = GetIntFromIDataReader(reader,"artikel id");
            im.ArtikelNummer = GetIntFromIDataReader(reader,"artikelnummer");
            im.Omschrijving = (string)reader["omschrijving"];
            im.Type = (string)reader["type"];
            im.Merk = (string)reader["merk"];
            im.Kleur = (string)reader["kleur"];
            im.Afmeting = (string)reader["afmeting"];
            im.Volume = GetDecimalFromIDataReader(reader,"volume");
            im.Opmerkingen = (string)reader["opmerkingen"];
            im.Afbeelding = (string)reader["afbeelding"];
            im.Aantal = GetIntFromIDataReader(reader,"aantal");
            im.Totaal = GetIntFromIDataReader(reader,"totaal");

            return im;
        }

        
    }
}
