﻿using Dapper;
using ReathUIv0._3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReathUIv0._3.Connections
{
    public class SqliteDatabaseAccess
    {
        public static List<Manufacturing> LoadMaterials()
        {
            using(SQLiteConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                List<Manufacturing> temp = new List<Manufacturing>();

                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"SELECT ManufacturingMaterial,MaterialProduction,Reused,OpenLoopSource,ClosedLoopSource FROM manufacturing";
                    cmd.Connection = cnn;


                    cnn.Open();

                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        float cost = (float)(double)rdr.GetValue(1);
                        float reuse = (float)(double)rdr.GetValue(2);
                        float openLoop = (float)(double)rdr.GetValue(3);
                        float closedLoop = (float)(double)rdr.GetValue(4);

                        

                        temp.Add(new Manufacturing(rdr.GetString(0), cost, reuse, openLoop, closedLoop));

                    }

                    cnn.Close();

                    return temp;
                }
                catch (SQLiteException)
                {
                    return null;
                }
            }
        }

        public static bool CheckData()
        {
            using(SQLiteConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"SELECT * FROM reusable_asset";
                    cmd.Connection = cnn;

                    cnn.Open();

                    int temp = 0;

                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        temp++;
                    }

                    if (temp > 0)
                    {
                        cnn.Close();
                        return true;
                    }
                    else
                    {
                        cnn.Close();
                        return false;
                    }

                    

                }
                catch (SQLiteException)
                {
                    return false;
                }
            }
        } 

        public static List<Disposal> LoadDisposal(string selectionMade)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                List<Disposal> temp = new List<Disposal>();

                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"SELECT MaterialOption,Conversion,Reuse,OpenLoop,ClosedLoop,Combustion,Composting,Landfill,AnaerobicDigestion FROM disposal WHERE MaterialOption = @SelectionMade";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add(new SQLiteParameter("@SelectionMade", selectionMade));
                    

                    cnn.Open();

                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        //float conversion = (float)(double)rdr.GetValue(1);
                        float reuse = (float)(double)rdr.GetValue(2);
                        float openLoop = (float)(double)rdr.GetValue(3);
                        float closedLoop = (float)(double)rdr.GetValue(4);
                        float combustion = (float)(double)rdr.GetValue(5);
                        float composting = (float)(double)rdr.GetValue(6);
                        float landfill = (float)(double)rdr.GetValue(7);
                        float anaerobicDigestion = (float)(double)rdr.GetValue(8);
                        

                        temp.Add(new Disposal(rdr.GetString(0), reuse,openLoop,closedLoop,combustion,composting,landfill,anaerobicDigestion));

                    }

                    cnn.Close();

                    return temp;
                }
                catch (SQLiteException)
                {
                    return null;
                }
            }
        }

        public static List<Disposal> LoadDisposal()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                List<Disposal> temp = new List<Disposal>();

                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"SELECT MaterialOption,Conversion,Reuse,OpenLoop,ClosedLoop,Combustion,Composting,Landfill,AnaerobicDigestion FROM disposal";
                    cmd.Connection = cnn;

                    cnn.Open();

                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        //float conversion = (float)(double)rdr.GetValue(1);
                        float reuse = (float)(double)rdr.GetValue(2);
                        float openLoop = (float)(double)rdr.GetValue(3);
                        float closedLoop = (float)(double)rdr.GetValue(4);
                        float combustion = (float)(double)rdr.GetValue(5);
                        float composting = (float)(double)rdr.GetValue(6);
                        float landfill = (float)(double)rdr.GetValue(7);
                        float anaerobicDigestion = (float)(double)rdr.GetValue(8);
                        

                        temp.Add(new Disposal(rdr.GetString(0), reuse, openLoop, closedLoop, combustion, composting, landfill, anaerobicDigestion));

                    }

                    cnn.Close();

                    return temp;
                }
                catch (SQLiteException)
                {
                    return null;
                }
            }
        }

        public static List<Transport> LoadTransport()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                List<Transport> temp = new List<Transport>();

                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"SELECT Vehicle,CarbonCost,wttConvFactor FROM freighting";
                    cmd.Connection = cnn;


                    cnn.Open();

                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        float cost = (float)(double)rdr.GetValue(1);
                        float wttFactor = (float)(double)rdr.GetValue(2);

                        temp.Add(new Transport(rdr.GetString(0),cost,wttFactor));

                    }

                    cnn.Close();

                    return temp;
                }
                catch (SQLiteException)
                {
                    return null;
                }
            }
        }

        public static List<string> RetreiveAssetAndId()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                List<string> temp = new List<string>();

                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"SELECT ReusableAssetId || '-' || AssetName FROM reusable_asset;";

                    cmd.Connection = cnn;
                    cnn.Open();

                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        temp.Add(rdr.GetString(0));
                    }

                    return temp;
                }
                catch (SQLiteException)
                {
                    return null;
                }
            }
        }
        
        /// Returns all Asset info from database inputting drop down collection
        public static ReusableAsset RetrieveAssets(string optionSelected)
        {
            using(SQLiteConnection cnn = new SQLiteConnection(LoadConnection()))
            {

                ReusableAsset temp = null;
                int recycleId = 0, reusableAssetId = 0;

                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"SELECT ReusableAssetId,SampleSize,DateRange,AssetName,UnitCost,UnitWeight,AssetCountryOfOrigin,PrimaryWeight,AuxiliarWeight,ReuseOccurence,MaximumReuses,AverageDistanceToReuse,PercentageOfManufacturingCarbon,RecycledId,PrimaryDisposalMethod,PrimaryCleaningMethod,AuxiliaryDisposalMethod,AuxiliaryCleaningMethod,PrimaryEmission,AuxiliaryEmission " +
                                        "FROM reusable_asset WHERE ReusableAssetId ||'-'||AssetName = @NameSelected;";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add(new SQLiteParameter("@NameSelected", optionSelected));

                    cnn.Open();

                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        reusableAssetId = rdr.GetInt32(0);
                        recycleId = rdr.GetInt32(13);

                        int sampleSize = rdr.GetInt32(1);
                        string dateRange = rdr.GetString(2);
                        string assetName = rdr.GetString(3);
                        float unitCost = rdr.GetFloat(4);
                        float unitWeight = rdr.GetFloat(5);
                        string assetCountryOfOrigin = rdr.GetString(6);
                        float primaryWeight = rdr.GetFloat(7);
                        float auxiliaryWeight = rdr.GetFloat(8);
                        double reuseOccurence = rdr.GetDouble(9);
                        int maximumReuses = rdr.GetInt32(10);
                        double averageDistanceToReuse = rdr.GetDouble(11);
                        float percentageOfManufacturingCarbon = (float)(double)rdr.GetValue(12);
                        string primaryDispoMethod = rdr.GetString(14);
                        string primaryCleaningMethod = rdr.GetString(15);
                        string auxiliaryDispoMethod = rdr.GetString(16);
                        string auxiliaryCleaningMethod = rdr.GetString(17);
                        string primaryMaterialEmission = rdr.GetString(18);
                        string auxiliaryMaterialEmission = rdr.GetString(19);



                        reusableAssetId = rdr.GetInt32(0);
                        recycleId = rdr.GetInt32(13);

                        temp = new ReusableAsset(sampleSize, dateRange, assetName, unitCost, unitWeight, assetCountryOfOrigin,primaryWeight,auxiliaryWeight,reuseOccurence, averageDistanceToReuse, maximumReuses, percentageOfManufacturingCarbon,primaryMaterialEmission,auxiliaryMaterialEmission);

                        temp.PrimaryDisposalMethod = ReusableAsset.StringToDisposalMethod(rdr.GetString(15));
                        temp.AuxiliaryDisposalMethod = ReusableAsset.StringToDisposalMethod(rdr.GetString(17));

                        temp.PrimaryManufacturingMethod = ReusableAsset.StringToManufacturingMethod(rdr.GetString(18));
                        temp.AuxiliaryManufacturingMethod = ReusableAsset.StringToManufacturingMethod(rdr.GetString(19));
                    }

                    rdr.Close();
                    cnn.Close();
                    
                    cmd.CommandText = @"SELECT IsRecycled,RecycledPercentage,RecycledCountryOfOrigin " +
                                        "FROM recycled WHERE RecycledId = @recycleId;";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add(new SQLiteParameter("@recycleId", recycleId));

                    cnn.Open();

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        temp.IsRecycled = Convert.ToBoolean(rdr.GetInt32(0));
                        temp.RecycledPercentage = rdr.GetInt32(1);
                        temp.RecycledCountryOfOrigin = rdr.GetString(2);
                    }

                    cnn.Close();

                    //return temp;

                    
                    cnn.Open();

                    var output = cnn.Query<string>($"SELECT ManufacturingMaterial FROM manufacturing JOIN reusable_asset_manufacturing USING(ManufacturingId) WHERE ReusableAssetId = '{ reusableAssetId }' ");
                    output.ToList();

                    if (output.Count() == 1)
                    {
                        temp.PrimaryMaterial = output.First();
                        
                        return temp;
                    }
                    else
                    {
                        
                        temp.PrimaryMaterial = output.First();
                        temp.AuxiliaryMaterial = output.ElementAt(1);
                        return temp;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        // Returns all Material data from database
        public static List<Manufacturing> LoadManufacturing()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                List<Manufacturing> temp = new List<Manufacturing>();

                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"SELECT ManufacturingMaterial,MaterialProduction,Reused,OpenLoopSource,ClosedLoopSource FROM manufacturing";
                    cmd.Connection = cnn;


                    cnn.Open();

                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        float production = (float)(double)rdr.GetValue(1);
                        float reused = (float)(double)rdr.GetValue(2);
                        float openLoop = (float)(double)rdr.GetValue(3);
                        float closedLoop = (float)(double)rdr.GetValue(4);

                        temp.Add(new Manufacturing(rdr.GetString(0), production, reused, openLoop, closedLoop));

                    }

                    cnn.Close();

                    return temp;
                }
                catch (SQLiteException)
                {
                    return null;
                }
            }
        }

        /// Returns all freighting data from database
        public static List<Transport> RetreiveTransport()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                List<Transport> temp = new List<Transport>();

                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"SELECT Vehicle,CarbonCost,wttConvFactor FROM freighting";
                    cmd.Connection = cnn;


                    cnn.Open();

                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        float cost = (float)(double)rdr.GetValue(1);
                        float convFactor = (float)(double)rdr.GetValue(2);

                        Console.WriteLine(rdr.GetValue(2));

                        temp.Add(new Transport(rdr.GetString(0), cost, convFactor));
                    }

                    cnn.Close();

                    return temp;
                }
                catch (SQLiteException)
                {
                    return null;
                }
            }
        }

        private static string LoadConnection()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public static List<int> RetrieveMaterialId(string material)
        {
            using(IDbConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                var output = cnn.Query<int>($"SELECT ManufacturingId FROM manufacturing WHERE ManufacturingMaterial = '{ material }' LIMIT 1");

                return output.ToList();
            }
        }

        public static List<int> RetrieveRecycleId()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                var output = cnn.Query<int>("SELECT RecycledId FROM recycled ORDER BY RecycledId DESC LIMIT 1");

                return output.ToList();
            }
        }

        public static bool SaveRecyle(ReusableAsset reusableAsset)
        {
            using(SQLiteConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"INSERT INTO recycled(IsRecycled,RecycledPercentage,RecycledCountryOfOrigin) VALUES (@IsRecycled,@RecycledPercentage,@RecycledCountryOfOrigin)";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add(new SQLiteParameter("@IsRecycled", reusableAsset.IsRecycled));
                    cmd.Parameters.Add(new SQLiteParameter("@RecycledPercentage", reusableAsset.RecycledPercentage));
                    cmd.Parameters.Add(new SQLiteParameter("@RecycledCountryOfOrigin", reusableAsset.RecycledCountryOfOrigin));

                    cnn.Open();

                    int temp = cmd.ExecuteNonQuery();

                    if(temp == 1)
                    {
                        cnn.Close();
                        return true;
                    }
                    else
                    {
                        cnn.Close();
                        return false;
                    }

                }
                catch (SQLiteException ex)
                {

                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }

        public static bool SaveReusableAsset(ReusableAsset reusableAsset,int recycleId)
        {
            using(SQLiteConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"INSERT INTO reusable_asset(SampleSize,DateRange,AssetName,UnitCost,UnitWeight,AssetCountryOfOrigin,PrimaryWeight,AuxiliarWeight,ReuseOccurence,MaximumReuses,AverageDistanceToReuse,PercentageOfManufacturingCarbon,RecycledId,PrimaryDisposalMethod,PrimaryCleaningMethod,AuxiliaryDisposalMethod,AuxiliaryCleaningMethod,PrimaryEmission,AuxiliaryEmission)" +
                    "VALUES (@SampleSize,@DateRange,@AssetName,@UnitCost,@UnitWeight,@AssetCountryOfOrigin,@PrimaryWeight,@AuxiliarWeight,@ReuseOccurence,@MaximumReuses,@AverageDistanceToReuse,@PercentageOfManufacturingCarbon,@recycleId,@primaryDisposalMethod,@primaryCleaningMethod,@auxiliaryDisposalMethod,@auxiliaryCleaningMethod,@primaryEmission,@auxiliaryEmission)";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add(new SQLiteParameter("@SampleSize", reusableAsset.SampleSize));
                    cmd.Parameters.Add(new SQLiteParameter("@DateRange", reusableAsset.DateRange));
                    cmd.Parameters.Add(new SQLiteParameter("@AssetName", reusableAsset.AssetName));
                    cmd.Parameters.Add(new SQLiteParameter("@UnitCost", reusableAsset.UnitCost));
                    cmd.Parameters.Add(new SQLiteParameter("@UnitWeight", reusableAsset.UnitWeight));
                    cmd.Parameters.Add(new SQLiteParameter("@AssetCountryOfOrigin", reusableAsset.AssetCountryOfOrigin));
                    cmd.Parameters.Add(new SQLiteParameter("@PrimaryWeight", reusableAsset.PrimaryWeight));
                    cmd.Parameters.Add(new SQLiteParameter("@primaryDisposalMethod", "Placeholder"));
                    cmd.Parameters.Add(new SQLiteParameter("@primaryCleaningMethod", ReusableAsset.DisposalMethodToString(reusableAsset.PrimaryDisposalMethod)));
                    cmd.Parameters.Add(new SQLiteParameter("@primaryEmission", ReusableAsset.ManufacturingMethodToString(reusableAsset.PrimaryManufacturingMethod)));
                    cmd.Parameters.Add(new SQLiteParameter("@AuxiliarWeight", reusableAsset.AuxiliaryWeight));
                    cmd.Parameters.Add(new SQLiteParameter("@auxiliaryDisposalMethod", "Placeholder"));
                    cmd.Parameters.Add(new SQLiteParameter("@auxiliaryCleaningMethod", ReusableAsset.DisposalMethodToString(reusableAsset.AuxiliaryDisposalMethod)));
                    cmd.Parameters.Add(new SQLiteParameter("@auxiliaryEmission", ReusableAsset.ManufacturingMethodToString(reusableAsset.AuxiliaryManufacturingMethod)));
                    cmd.Parameters.Add(new SQLiteParameter("@ReuseOccurence", reusableAsset.ReuseOccurence));
                    cmd.Parameters.Add(new SQLiteParameter("@MaximumReuses", reusableAsset.MaximumReuses));
                    cmd.Parameters.Add(new SQLiteParameter("@AverageDistanceToReuse", reusableAsset.AverageDistanceToReuse));
                    cmd.Parameters.Add(new SQLiteParameter("@PercentageOfManufacturingCarbon", reusableAsset.PercentageOfManufacturingCarbon));
                    cmd.Parameters.Add(new SQLiteParameter("@recycleId", recycleId));



                    cnn.Open();

                    int temp = cmd.ExecuteNonQuery();

                    if(temp == 1)
                    {
                        cnn.Close();
                        return true;
                    }
                    else
                    {
                        cnn.Close();
                        return false;
                    }

                }
                catch (SQLiteException)
                {
                    cnn.Close();
                    return false;
                }

            }

        }

        public static bool SaveReusableAssetManufacturing(int reusableAssetId,int primaryId,int auxiliarId)
        {
            using(SQLiteConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"INSERT INTO reusable_asset_manufacturing(ReusableAssetId,ManufacturingId) VALUES (@reusableAssetId,@primaryId)";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add(new SQLiteParameter("@reusableAssetId", reusableAssetId));
                    cmd.Parameters.Add(new SQLiteParameter("@primaryId", primaryId));

                    cnn.Open();

                    int temp = cmd.ExecuteNonQuery();

                    if(temp == 1)
                    {
                        if(auxiliarId != 0)
                        {
                            cnn.Close();

                            cmd.CommandText = @"INSERT INTO reusable_asset_manufacturing(ReusableAssetId,ManufacturingId) VALUES (@reusableAssetId,@auxiliarId)";
                            cmd.Parameters.Add(new SQLiteParameter("@auxiliarId", auxiliarId));

                            cnn.Open();

                            temp = cmd.ExecuteNonQuery();

                            if (temp == 1)
                            {
                                cnn.Close();
                                return true;
                            }
                        }
                        cnn.Close();
                        return true;

                    }
                    else
                    {
                        cnn.Close();
                        return false;
                    }

                }
                catch (SQLiteException)
                {
                    return false;
                }

            }
        }

        public static List<int> RetreiveReusableAssetId()
        {
            using(IDbConnection cnn = new SQLiteConnection(LoadConnection()))
            {
                var output = cnn.Query<int>("SELECT ReusableAssetId FROM reusable_asset ORDER BY ReusableAssetId DESC LIMIT 1");

                return output.ToList();
            }
        } 
    }
}
