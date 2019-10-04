using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

public enum Reglament_owner { AOFOMS, SMEV12, SMEV13, FFOMS, NULL }
public class Reglament_link
{
    public string filemask;
    public string nameSchema;
    public string version;
    public Type schemaClassRoot;
    public Reglament_owner reglament_owner;
    public string prefixFile;
    public string nameSpace;
    public string comment;
    public string filename;
    public Reglament_link(string _filemask, string _nameSchema, string _version, Type _schemaClass, Reglament_owner _reglament_owner, string _prefixFile, string _Namespace, string _comment)
    {
        filemask = _filemask;
        nameSchema = _nameSchema;
        version = _version;
        schemaClassRoot = _schemaClass;
        reglament_owner = _reglament_owner;
        prefixFile = _prefixFile;
        nameSpace = _Namespace;
        comment = _comment;
        filename = string.Empty;
    }
}
public class ReglamentLinker
// Класс определяющий связи файлов пакетов данных со схемами данных
{
    public Reglament_link link = null;
    #region SchemaLink Таблица/справочник
    List<Reglament_link> reglament_links = new List<Reglament_link>
        // Таблица/справочник
        {
            //-------------------------- Схемы АОФОМС 
            // Профилактика (диспансеризация)
            new Reglament_link("sim#_*.xml", "si_schema_1_0", "1.0" , typeof(Schemes_AOFOMS.si_schema_1_0.PROFIL), Reglament_owner.AOFOMS, null, null, "Диспансеризация, формирование планов")
            ,new Reglament_link("sim#_*.xml", "si_schema_1_1", "1.1" , typeof(Schemes_AOFOMS.si_schema_1_1.PROFIL), Reglament_owner.AOFOMS, null, null, "Диспансеризация, формирование планов")
            // Диспансерное наблюдение
            //,new Reglament_link("zldnm#_*.xml", "zldn_schema_1_0", "1.0" , typeof(Schemes_AOFOMS.zldn_schema_1_0.ZLDN), Reglament_owner.AOFOMS, null, null, "Диспансерное наблюдение")
            //,new Reglament_link("zldnm#_*.xml", "zldn_schema_2_0", "2.0" , typeof(Schemes_AOFOMS.zldn_schema_2_0.ZLDN), Reglament_owner.AOFOMS, null, null, "Диспансерное наблюдение")
            ,new Reglament_link("zldnm#_*.xml", "zldn_schema_2_1", "2.1" , typeof(Schemes_AOFOMS.zldn_schema_2_1.ZLDN), Reglament_owner.AOFOMS, null, null, "Диспансерное наблюдение")
            //-------------------------- Схемы ФФОМС 
            // 	F003. Единый реестр медицинских организаций, осуществляющих деятельность в сфере обязательного медицинского страхования.Реестр МО
            ,new Reglament_link("lib_f003_*.xml", "f003_schema_1_0_1", "1.0.1", typeof(Schemes_FFOMS.f003_schema_1_0_1), Reglament_owner.FFOMS, "F003", null, "F003. Единый реестр медицинских организаций, осуществляющих деятельность в сфере обязательного медицинского страхования.Реестр МО")
            //-------------------------- Схемы СМЭВ
            ,new Reglament_link("smev12-{*}.xml", "smev12_schema_POLIS", "1.0.0", typeof(VS01112v001_TABL00), Reglament_owner.SMEV12, "POLIS", "http://ffoms.ru/ExecutionMedicalInsurancePolicy/1.0.0", "Прием заявления о выборе СМО в ТФОМС")
            ,new Reglament_link("smev12-{*}.xml", "smev12_schema_POLIS-KUTFOMS", "1.0.1", typeof(VS00643v002_MZRV01), Reglament_owner.SMEV12, "POLIS-KUTFOMS", "http://egisz.rosminzdrav.ru/ExecutionMedicalInsurancePolicy/order/event/1.0.1", "Сведения для приема событий электронного заявления о выборе (замене) СМО на КУТФОМС")
            ,new Reglament_link("smev12-{*}.xml", "smev12_schema_USLUGI", "1.0.0", typeof(VS01113v001_TABL00), Reglament_owner.SMEV12, "USLUGI", "http://ffoms.ru/GetInsuredRenderedMedicalServices/1.0.0", "Сведения ТФОМС об оказанных медицинских услугах и их стоимости")
            //FATAL
            ,new Reglament_link("smev13-{*}.xml", "smev13_schema_FATALZP", "4.0.0", typeof(VS01285v001_TABL00), Reglament_owner.SMEV13, "FATALZP-4.0.0", "urn://x-artefacts-zags-fatalzp/root/112-25/4.0.0", "Сведения из ЕГР ЗАГС о государственной регистрации смерти")
            ,new Reglament_link("smev13-{*}.xml", "smev13_schema_FATALZP", "4.0.1", typeof(VS01285v002_TABL00), Reglament_owner.SMEV13, "FATALZP-4.0.1", "urn://x-artefacts-zags-fatalzp/root/112-25/4.0.1", "Сведения из ЕГР ЗАГС о государственной регистрации смерти")

            ,new Reglament_link("smev13-{*}.xml", "smev13_schema_ROGDZP", "4.0.0", typeof(VS01287v001_TABL00), Reglament_owner.SMEV13, "ROGDZP", "urn://x-artefacts-zags-rogdzp/root/112-23/4.0.0", "Сведения и ЕГР ЗАГС о государственной регистрации рождения")
            ,new Reglament_link("smev13-{*}.xml", "smev13_schema_PERNAMEZP", "4.0.0", typeof(VS01284v001_TABL00), Reglament_owner.SMEV13, "PERNAMEZP", "urn://x-artefacts-zags-pernamezp/root/112-24/4.0.0", "Сведения из ЕГР ЗАГС о государственной регистрации перемены имени")
            ,new Reglament_link("smev13-{*}.xml", "smev13_schema_SNILS", "1.0.1", typeof(VS00648v001_PFR001), Reglament_owner.SMEV13, "SNILS", "http://kvs.pfr.com/snils-by-additionalData/1.0.1", "Сведение «Предоставление страхового номера индивидуального лицевого счёта (СНИЛС) застрахованного лица с учётом дополнительных сведений о месте рождения, документе, удостоверяющем личность» ID вида сведений в ФРГУ")
            ,new Reglament_link("smev12-{*}.xml", "smev12_schema_INVALID", "1.0.3", typeof(VS00291v004_PFRF01), Reglament_owner.SMEV12, "INVALID", "http://kvs.fri.com/extraction-invalid-data/1.0.3", "Выписка сведений об инвалиде")
        };
    #endregion

    public void getLink(Type schemaClassRoot = null, string nameSchema = null, string version = null, string nameFile = null, string prefixFile = null)
    /* получение Связи с регламентом*/
    {
        try
        {
            link = null;

            if (schemaClassRoot != null)
            {
                foreach (Reglament_link reglament_link in reglament_links)
                    if (reglament_link.schemaClassRoot == schemaClassRoot)
                    {
                        link = reglament_link; break;
                    }
            }
            else if (nameSchema != null)
            {
                foreach (Reglament_link reglament_link in reglament_links)
                    if (((reglament_link.nameSchema == null) ? "" : reglament_link.nameSchema) == nameSchema
                        && ((version != null) ? ((reglament_link.version == null) ? "" : reglament_link.version) == version : true))
                    {
                        link = reglament_link; break;
                    }
            }
            else if (nameFile != null)
            {
                foreach (Reglament_link reglament_link in reglament_links)
                    if (fileMatchMultiMasks(nameFile, ((string)reglament_link.filemask == null ? "" : reglament_link.filemask).Replace('#', '*'))
                        && ((version != null) ? ((reglament_link.version == null) ? "" : reglament_link.version) == version : true))
                    {
                        link = reglament_link;
                        link.filename = nameFile;
                        break;
                    }
            }
            else if (prefixFile != null)
            {
                foreach (Reglament_link reglament_link in reglament_links)
                    if (((reglament_link.prefixFile == null) ? "" : reglament_link.prefixFile) == prefixFile)
                    {
                        link = reglament_link; break;
                    }
            }
        }
        catch { }
    }
    public void getLink(ref XmlElement xmlElement)
    /* получение Связи с регламентом*/
    {
        link = null;
        if (xmlElement == null) return;
        foreach (Reglament_link reglament_link in reglament_links)
            if (xmlElement.GetPrefixOfNamespace((reglament_link.nameSpace == null) ? "" : reglament_link.nameSpace) != "")
                link = reglament_link;
    }
    public string getMnemonic()
    {
        try
        {
            int index1 = link.filemask.IndexOf("#");
            string separator = link.filemask.Substring(index1 + 1, 1);
            int index_separator = link.filename.IndexOf(separator);
            return link.filename.Substring(index1, index_separator - index1);
        }
        catch
        {
            return string.Empty;
        };
    }
    private bool fileMatchMask(string fileName, string fileMask)
    // сопоставление имени файла с маской
    {
        Regex mask = new Regex(
            '^' +
            fileMask
                .Replace(".", "[.]")
                .Replace("*", ".*")
                .Replace("?", ".")
            + '$',
            RegexOptions.IgnoreCase);
        return mask.IsMatch(fileName);
    }
    private bool fileMatchMultiMasks(string fileName, string fileMasks)
    // сопоставление имени файла с группой масок
    {
        return fileMasks
            .Split(new string[] { "\r\n", "\n", ",", "|", " " },
                StringSplitOptions.RemoveEmptyEntries)
            .Any(fileMask => fileMatchMask(fileName, fileMask));
    }
}
