﻿/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
MERGE INTO [dbo].[Medicine] AS target
USING (
    VALUES 
        ('Aspirin', 'Acetylsalicylic Acid', 'Tablet', '500 mg', 'Bayer', 'BATCH001', '2025-12-31T00:00:00Z', 5.99, 100),
        ('Ibuprofen', 'Ibuprofen', 'Tablet', '400 mg', 'Advil', 'BATCH002', '2025-11-30T00:00:00Z', 7.49, 150),
        ('Paracetamol', 'Acetaminophen', 'Tablet', '500 mg', 'Tylenol', 'BATCH003', '2025-10-31T00:00:00Z', 4.99, 200),
        ('Amoxicillin', 'Amoxicillin', 'Capsule', '500 mg', 'Amoxil', 'BATCH004', '2025-09-30T00:00:00Z', 12.99, 80),
        ('Metformin', 'Metformin', 'Tablet', '850 mg', 'Glucophage', 'BATCH005', '2025-08-31T00:00:00Z', 9.99, 120),
        ('Lisinopril', 'Lisinopril', 'Tablet', '10 mg', 'Prinivil', 'BATCH006', '2025-07-31T00:00:00Z', 11.49, 90),
        ('Simvastatin', 'Simvastatin', 'Tablet', '20 mg', 'Zocor', 'BATCH007', '2025-06-30T00:00:00Z', 14.99, 110),
        ('Atorvastatin', 'Atorvastatin', 'Tablet', '40 mg', 'Lipitor', 'BATCH008', '2025-05-31T00:00:00Z', 15.99, 130),
        ('Omeprazole', 'Omeprazole', 'Capsule', '20 mg', 'Prilosec', 'BATCH009', '2025-04-30T00:00:00Z', 8.99, 140),
        ('Levothyroxine', 'Levothyroxine', 'Tablet', '50 mcg', 'Synthroid', 'BATCH010', '2025-03-31T00:00:00Z', 19.99, 70),
        ('Amlodipine', 'Amlodipine', 'Tablet', '5 mg', 'Norvasc', 'BATCH011', '2025-02-28T00:00:00Z', 10.99, 160),
        ('Ciprofloxacin', 'Ciprofloxacin', 'Tablet', '500 mg', 'Cipro', 'BATCH012', '2025-01-31T00:00:00Z', 13.49, 75),
        ('Hydrochlorothiazide', 'Hydrochlorothiazide', 'Tablet', '25 mg', 'Microzide', 'BATCH013', '2024-12-31T00:00:00Z', 6.99, 85),
        ('Furosemide', 'Furosemide', 'Tablet', '40 mg', 'Lasix', 'BATCH014', '2024-11-30T00:00:00Z', 5.49, 95),
        ('Gabapentin', 'Gabapentin', 'Capsule', '300 mg', 'Neurontin', 'BATCH015', '2024-10-31T00:00:00Z', 12.49, 65),
        ('Sertraline', 'Sertraline', 'Tablet', '50 mg', 'Zoloft', 'BATCH016', '2024-09-30T00:00:00Z', 15.49, 55),
        ('Fluoxetine', 'Fluoxetine', 'Capsule', '20 mg', 'Prozac', 'BATCH017', '2024-08-31T00:00:00Z', 14.49, 45),
        ('Citalopram', 'Citalopram', 'Tablet', '20 mg', 'Celexa', 'BATCH018', '2024-07- 31T00:00:00Z', 13.99, 50),
        ('Escitalopram', 'Escitalopram', 'Tablet', '10 mg', 'Lexapro', 'BATCH019', '2024-06-30T00:00:00Z', 16.99, 40),
        ('Bupropion', 'Bupropion', 'Tablet', '150 mg', 'Wellbutrin', 'BATCH020', '2024-05-31T00:00:00Z', 18.49, 30),
        ('Venlafaxine', 'Venlafaxine', 'Capsule', '75 mg', 'Effexor', 'BATCH021', '2024-04-30T00:00:00Z', 17.99, 25),
        ('Duloxetine', 'Duloxetine', 'Capsule', '30 mg', 'Cymbalta', 'BATCH022', '2024-03-31T00:00:00Z', 19.49, 20),
        ('Clonazepam', 'Clonazepam', 'Tablet', '0.5 mg', 'Klonopin', 'BATCH023', '2024-02-29T00:00:00Z', 8.49, 15),
        ('Lorazepam', 'Lorazepam', 'Tablet', '1 mg', 'Ativan', 'BATCH024', '2024-01-31T00:00:00Z', 9.99, 10),
        ('Alprazolam', 'Alprazolam', 'Tablet', '0.5 mg', 'Xanax', 'BATCH025', '2023-12-31T00:00:00Z', 7.99, 5),
        ('Metoprolol', 'Metoprolol', 'Tablet', '50 mg', 'Lopressor', 'BATCH026', '2023-11-30T00:00:00Z', 11.99, 60),
        ('Propranolol', 'Propranolol', 'Tablet', '40 mg', 'Inderal', 'BATCH027', '2023-10-31T00:00:00Z', 10.49, 70),
        ('Carvedilol', 'Carvedilol', 'Tablet', '12.5 mg', 'Coreg', 'BATCH028', '2023-09-30T00:00:00Z', 12.99, 80),
        ('Diltiazem', 'Diltiazem', 'Capsule', '120 mg', 'Cardizem', 'BATCH029', '2023-08-31T00:00:00Z', 13.49, 90),
        ('Verapamil', 'Verapamil', 'Tablet', '80 mg', 'Calan', 'BATCH030', '2023-07-31T00:00:00Z', 14.99, 100),
        ('Nitroglycerin', 'Nitroglycerin', 'Sublingual Tablet', '0.4 mg', 'Nitrostat', 'BATCH031', '2023-06-30T00:00:00Z', 5.99, 110),
        ('Digoxin', 'Digoxin', 'Tablet', '0.25 mg', 'Lanoxin', 'BATCH032', '2023-05-31T00:00:00Z', 6.49, 120),
        ('Warfarin', 'Warfarin', 'Tablet', '5 mg', 'Coumadin', 'BATCH033', '2023-04-30T00:00:00Z', 8.99, 130),
        ('Rivaroxaban', 'Rivaroxaban', 'Tablet', '20 mg', 'Xarelto', 'BATCH034', '2023-03-31T00:00:00Z', 19.99, 140),
        ('Apixaban', 'Apixaban', 'Tablet', '5 mg', 'Eliquis', 'BATCH035', '2023-02-28T00:00:00Z', 18.49, 150),
        ('Clopidogrel', 'Clopidogrel', 'Tablet', '75 mg', 'Plavix', 'BATCH036', '2023-01-31T00:00:00Z', 17.99, 160),
        ('Aspirin', 'Acetylsalicylic Acid', 'Tablet', '81 mg', 'Bayer', 'BATCH037', '2022-12-31T00:00:00Z', 4.99, 170),
        ('Montelukast', 'Montelukast', 'Tablet', '10 mg', 'Singulair', 'BATCH038', '2022-11-30T00:00:00Z', 15.49, 180),
        ('Levocetirizine', 'Levocetirizine', 'Tablet', '5 mg', 'Xyzal', 'BATCH039', '2022-10-31T00:00:00Z', 12.99, 190),
        ('Cetirizine', 'Cetirizine', 'Tablet', '10 mg', 'Zyrtec', 'BATCH040', '2022-09-30T00:00:00Z', 11.49, 200),
        ('Diphenhydramine', 'Diphenhydramine', 'Tablet', '25 mg', 'Benadryl', 'BATCH041', '2022-08-31T00:00:00Z', 6.99, 210),
        ('Loratadine', 'Loratadine', 'Tablet', '10 mg', 'Claritin', 'BATCH042', '2022-07-31T00:00:00Z', 7.49, 220),
        ('Fluticasone', 'Fluticasone', 'Nasal Spray', '50 mcg', 'Flonase', 'BATCH043', '2022-06-30T00:00:00Z', 14.99, 230),
        ('Budesonide', 'Budesonide', 'Nasal Spray', '32 mcg', 'Rhinocort', 'BATCH044', '2022-05-31T00:00:00Z', 13.49, 240),
        ('Montelukast', 'Montelukast', 'Chewable Tablet', '4 mg', 'Singulair', 'BATCH045', '2022-04-30T00:00:00Z', 15.99, 250),
        ('Albuterol', 'Albuterol', 'Inhaler', '90 mcg', 'Ventolin', 'BATCH046', '2022-03-31T00:00:00Z', 29.99, 260),
        ('Ipratropium', 'Ipratropium', 'Inhaler', '20 mcg', 'Atrovent', 'BATCH047', '2022-02-28T00:00:00Z', 25.99, 270),
        ('Fluticasone/Salmeterol', 'Fluticasone/Salmeterol', 'Inhaler', '250/50 mcg', 'Advair', 'BATCH048', '2022-01-31T00:00:00Z', 59.99, 280),
        ('Brompheniramine', 'Brompheniramine', 'Tablet', '4 mg', 'Dimetapp', 'BATCH049', '2021-12-31T00:00:00Z', 8.49, 290),
        ('Dextromethorphan', 'Dextromethorphan', 'Cough Syrup', '10 mg/5 mL', 'Robitussin', 'BATCH050', '2021-11-30T00:00:00Z', 9.99, 300),
        ('Guaifenesin', 'Guaifenesin', 'Cough Syrup', '100 mg/5 mL', 'Mucinex', 'BATCH051', '2021-10-31T00:00:00Z', 10.49, 310),
        ('Phenylephrine', 'Phenylephrine', 'Nasal Spray', '0.5%', 'Sudafed PE', 'BATCH052', '2021-09-30T00:00:00Z', 7.99, 320),
        ('Loperamide', 'Loperamide', 'Capsule', '2 mg', 'Imodium', 'BATCH053', '2021-08-31T00:00:00Z', 6.49, 330),
        ('Omeprazole', 'Omeprazole', 'Tablet', '20 mg', 'Prilosec', 'BATCH054', '2021-07-31T00:00:00Z', 8.99, 340),
        ('Esomeprazole', 'Esomeprazole', 'Tablet', '20 mg', 'Nexium', 'BATCH055', '2021-06-30T00:00:00Z', 12.99, 350),
        ('Ranitidine', 'Ranitidine', 'Tablet', '150 mg', 'Zantac', 'BATCH056', '2021-05-31T00:00:00Z', 9.49, 360),
        ('Famotidine', 'Famotidine', 'Tablet', '20 mg', 'Pepcid', 'BATCH057', '2021-04-30T00:00:00Z', 8.99, 370),
        ('Bismuth Subsalicylate', 'Bismuth Subsalicylate', 'Tablet', '262 mg', 'Pepto-Bismol', 'BATCH058', '2021-03-31T00:00:00Z', 6.99, 380),
        ('Lansoprazole', 'Lansoprazole', 'Capsule', '30 mg', 'Prevacid', 'BATCH059', '2021-02-28T00:00:00Z', 14.49, 390),
        ('Dexlansoprazole', 'Dexlansoprazole', 'Capsule', '30 mg', 'Dexilant', 'BATCH060', '2021-01-31T00:00:00Z', 15.99, 400),
        ('Atenolol', 'Atenolol', 'Tablet', '50 mg', 'Tenormin', 'BATCH061', '2020-12-31T00:00:00Z', 10.99, 410),
        ('Sotalol', 'Sotalol', 'Tablet', '80 mg', 'Betapace', 'BATCH062', '2020-11-30T00:00:00Z', 12.49, 420),
        ('Dronedarone', 'Dronedarone', 'Tablet', '400 mg', 'Multaq', 'BATCH063', '2020-10-31T00:00:00Z', 18.99, 430),
        ('Ranolazine', 'Ranolazine', 'Tablet', '500 mg', 'Ranexa', 'BATCH064', '2020-09-30T00:00:00Z', 19.49, 440),
        ('Nitrofurantoin', 'Nitrofurantoin', 'Capsule', '100 mg', 'Macrobid', 'BATCH065', '2020-08-31T00:00:00Z', 14.99, 450),
        ('Trimethoprim/Sulfamethoxazole', 'Trimethoprim/Sulfamethoxazole', 'Tablet', '160/800 mg', 'Bactrim', 'BATCH066', '2020-07-31T00:00:00Z', 9.99, 460),
        ('Cefalexin', 'Cefalexin', 'Capsule', '500 mg', 'Keflex', 'BATCH067', '2020-06-30T00:00:00Z', 12.99, 470),
        ('Clindamycin', 'Clindamycin', 'Capsule', '300 mg', 'Cleocin', 'BATCH068', '2020-05-31T00:00:00Z', 15.49, 480),
        ('Metronidazole', 'Metronidazole', 'Tablet', '500 mg', 'Flagyl', 'BATCH069', '2020-04-30T00:00:00Z', 10.49, 490),
        ('Vancomycin', 'Vancomycin', 'Capsule', '125 mg', 'Vancocin', 'BATCH070', '2020-03-31T00:00:00Z', 19.99, 500),
        ('Azithromycin', 'Azithromycin', 'Tablet', '250 mg', 'Zithromax', 'BATCH071', '2020-02-29T00:00:00Z', 14.99, 510),
        ('Ciprofloxacin', 'Ciprofloxacin', 'Tablet', '750 mg', 'Cipro', 'BATCH072', '2020-01-31T00:00:00Z', 13.49, 520),
        ('Levofloxacin', 'Levofloxacin', 'Tablet', '500 mg', 'Levaquin', 'BATCH073', '2019-12-31T00:00:00Z', 15.99, 530),
        ('Moxifloxacin', 'Moxifloxacin', 'Tablet', '400 mg', 'Avelox', 'BATCH074', '2019-11-30T00:00:00Z', 18.49, 540),
        ('Doxycycline', 'Doxycycline', ' Capsule', '100 mg', 'Vibramycin', 'BATCH075', '2019-10-31T00:00:00Z', 12.99, 550),
        ('Tetracycline', 'Tetracycline', 'Capsule', '500 mg', 'Sumycin', 'BATCH076', '2019-09-30T00:00:00Z', 10.49, 560),
        ('Minocycline', 'Minocycline', 'Capsule', '100 mg', 'Minocin', 'BATCH077', '2019-08-31T00:00:00Z', 14.99, 570),
        ('Clarithromycin', 'Clarithromycin', 'Tablet', '500 mg', 'Biaxin', 'BATCH078', '2019-07-31T00:00:00Z', 16.49, 580),
        ('Rifampin', 'Rifampin', 'Capsule', '300 mg', 'Rifadin', 'BATCH079', '2019-06-30T00:00:00Z', 19.99, 590),
        ('Isoniazid', 'Isoniazid', 'Tablet', '300 mg', 'Nydrazid', 'BATCH080', '2019-05-31T00:00:00Z', 8.99, 600),
        ('Pyrazinamide', 'Pyrazinamide', 'Tablet', '500 mg', 'PZA', 'BATCH081', '2019-04-30T00:00:00Z', 7.49, 610),
        ('Ethambutol', 'Ethambutol', 'Tablet', '400 mg', 'Myambutol', 'BATCH082', '2019-03-31T00:00:00Z', 9.99, 620),
        ('Acyclovir', 'Acyclovir', 'Tablet', '400 mg', 'Zovirax', 'BATCH083', '2019-02-28T00:00:00Z', 12.49, 630),
        ('Valacyclovir', 'Valacyclovir', 'Tablet', '500 mg', 'Valtrex', 'BATCH084', '2019-01-31T00:00:00Z', 14.99, 640),
        ('Oseltamivir', 'Oseltamivir', 'Capsule', '75 mg', 'Tamiflu', 'BATCH085', '2018-12-31T00:00:00Z', 18.49, 650),
        ('Zanamivir', 'Zanamivir', 'Inhaler', '5 mg', 'Relenza', 'BATCH086', '2018-11-30T00:00:00Z', 22.99, 660),
        ('Ribavirin', 'Ribavirin', 'Capsule', '200 mg', 'Copegus', 'BATCH087', '2018-10-31T00:00:00Z', 19.99, 670),
        ('Sofosbuvir', 'Sofosbuvir', 'Tablet', '400 mg', 'Sovaldi', 'BATCH088', '2018-09-30T00:00:00Z', 28.99, 680),
        ('Ledipasvir/Sofosbuvir', 'Ledipasvir/Sofosbuvir', 'Tablet', '90/400 mg', 'Harvoni', 'BATCH089', '2018-08-31T00:00:00Z', 45.99, 690),
        ('Daclatasvir', 'Daclatasvir', 'Tablet', '60 mg', 'Daklinza', 'BATCH090', '2018-07-31T00:00:00Z', 39.99, 700),
        ('Elbasvir/Grazoprevir', 'Elbasvir/Grazoprevir', 'Tablet', '50/100 mg', 'Zepatier', 'BATCH091', '2018-06-30T00:00:00Z', 42.99, 710),
        ('Bortezomib', 'Bortezomib', 'Injection', '3.5 mg', 'Velcade', 'BATCH092', '2018-05-31T00:00:00Z', 350.00, 720),
        ('Rituximab', 'Rituximab', 'Injection', '100 mg', 'Rituxan', 'BATCH093', '2018-04-30T00:00:00Z', 900.00, 730 ),
        ('Trastuzumab', 'Trastuzumab', 'Injection', '440 mg', 'Herceptin', 'BATCH094', '2018-03-31T00:00:00Z', 750.00, 740),
        ('Nivolumab', 'Nivolumab', 'Injection', '100 mg', 'Opdivo', 'BATCH095', '2018-02-28T00:00:00Z', 1200.00, 750),
        ('Pembrolizumab', 'Pembrolizumab', 'Injection', '100 mg', 'Keytruda', 'BATCH096', '2018-01-31T00:00:00Z', 1500.00, 760),
        ('Atezolizumab', 'Atezolizumab', 'Injection', '1200 mg', 'Tecentriq', 'BATCH097', '2017-12-31T00:00:00Z', 1600.00, 770),
        ('Bevacizumab', 'Bevacizumab', 'Injection', '100 mg', 'Avastin', 'BATCH098', '2017-11-30T00:00:00Z', 900.00, 780),
        ('Cetuximab', 'Cetuximab', 'Injection', '100 mg', 'Erbitux', 'BATCH099', '2017-10-31T00:00:00Z', 800.00, 790),
        ('Rituximab', 'Rituximab', 'Injection', '500 mg', 'Rituxan', 'BATCH100', '2017-09-30T00:00:00Z', 850.00, 800),
        ('Imatinib', 'Imatinib', 'Tablet', '100 mg', 'Gleevec', 'BATCH101', '2017-08-31T00:00:00Z', 250.00, 810),
        ('Dasatinib', 'Dasatinib', 'Tablet', '100 mg', 'Sprycel', 'BATCH102', '2017-07-31T00:00:00Z', 300.00, 820),
        ('Nilotinib', 'Nilotinib', 'Capsule', '150 mg', 'Tasigna', 'BATCH103', '2017-06-30T00:00:00Z', 350.00, 830),
        ('Bosutinib', 'Bosutinib', 'Tablet', '100 mg', 'Bosulif', 'BATCH104', '2017-05-31T00:00:00Z', 400.00, 840),
        ('Ponatinib', 'Ponatinib', 'Tablet', '15 mg', 'Iclusig', 'BATCH105', '2017-04-30T00:00:00Z', 450.00, 850),
        ('Erlotinib', 'Erlotinib', 'Tablet', '150 mg', 'Tarceva', 'BATCH106', '2017-03-31T00:00:00Z', 500.00, 860),
        ('Gefitinib', 'Gefitinib', 'Tablet', '250 mg', 'Iressa', 'BATCH107', '2017-02-28T00:00:00Z', 550.00, 870),
        ('Crizotinib', 'Crizotinib', 'Capsule', '250 mg', 'Xalkori', 'BATCH108', '2017-01-31T00:00:00Z', 600.00, 880),
        ('Ceritinib', 'Ceritinib', 'Capsule', '150 mg', 'Zykadia', 'BATCH109', '2016-12-31T00:00:00Z', 650.00, 890),
        ('Alectinib', 'Alectinib', 'Capsule', '600 mg', 'Alecensa', 'BATCH110', '2016-11-30T00:00:00Z', 700.00, 900),
        ('Brigatinib', 'Brigatinib', 'Tablet', '90 mg', 'Alunbrig', 'BATCH111', '2016-10-31T00:00:00Z', 750.00, 910),
        ('Lorlatinib', 'Lorlatinib', 'Tablet', '100 mg', 'Lorbrena', 'BATCH112', '2016-09-30T00:00:00Z', 800.00, 920),
        ('Olaparib', 'Olaparib', 'Tablet', '100 mg ', 'Lynparza', 'BATCH113', '2016-08-31T00:00:00Z', 900.00, 930),
        ('Rucaparib', 'Rucaparib', 'Tablet', '200 mg', 'Rubraca', 'BATCH114', '2016-07-31T00:00:00Z', 950.00, 940),
        ('Niraparib', 'Niraparib', 'Capsule', '100 mg', 'Zejula', 'BATCH115', '2016-06-30T00:00:00Z', 1000.00, 950),
        ('Talazoparib', 'Talazoparib', 'Capsule', '1 mg', 'Talzenna', 'BATCH116', '2016-05-31T00:00:00Z', 1100.00, 960),
        ('Abiraterone', 'Abiraterone', 'Tablet', '250 mg', 'Zytiga', 'BATCH117', '2016-04-30T00:00:00Z', 1200.00, 970),
        ('Enzalutamide', 'Enzalutamide', 'Capsule', '40 mg', 'Xtandi', 'BATCH118', '2016-03-31T00:00:00Z', 1300.00, 980),
        ('Apalutamide', 'Apalutamide', 'Tablet', '240 mg', 'Erleada', 'BATCH119', '2016-02-29T00:00:00Z', 1400.00, 990),
        ('Darolutamide', 'Darolutamide', 'Tablet', '300 mg', 'Nubeqa', 'BATCH120', '2016-01-31T00:00:00Z', 1500.00, 1000),
        ('Cabozantinib', 'Cabozantinib', 'Capsule', '60 mg', 'Cabometyx', 'BATCH121', '2015-12-31T00:00:00Z', 1600.00, 1010),
        ('Lenvatinib', 'Lenvatinib', 'Capsule', '10 mg', 'Lenvima', 'BATCH122', '2015-11-30T00:00:00Z', 1700.00, 1020),
        ('Regorafenib', 'Regorafenib', 'Tablet', '40 mg', 'Stivarga', 'BATCH123', '2015-10-31T00:00:00Z', 1800.00, 1030),
        ('Sorafenib', 'Sorafenib', 'Tablet', '200 mg', 'Nexavar', 'BATCH124', '2015-09-30T00:00:00Z', 1900.00, 1040),
        ('Vandetanib', 'Vandetanib', 'Tablet', '100 mg', 'Caprelsa', 'BATCH125', '2015-08-31T00:00:00Z', 2000.00, 1050),
        ('Axitinib', 'Axitinib', 'Tablet', '1 mg', 'Inlyta', 'BATCH126', '2015-07-31T00:00:00Z', 2100.00, 1060),
        ('Nintedanib', 'Nintedanib', 'Capsule', '100 mg', 'Ofev', 'BATCH127', '2015-06-30T00:00:00Z', 2200.00, 1070),
        ('Ruxolitinib', 'Ruxolitinib', 'Tablet', '5 mg', 'Jakafi', 'BATCH128', '2015-05-31T00:00:00Z', 2300.00, 1080),
        ('Tofacitinib', 'Tofacitinib', 'Tablet', '5 mg', 'Xeljanz', 'BATCH129', '2015-04-30T00:00:00Z', 2400.00, 1090),
        ('Baricitinib', 'Baricitinib', 'Tablet', '2 mg', 'Olumiant', 'BATCH130', '2015-03-31T00:00:00Z', 2500.00, 1100),
        ('Upadacitinib', 'Upadacitinib', 'Tablet', '15 mg', 'Rinvoq', 'BATCH131', '2015-02-28T00:00:00Z', 2600.00,1200),
        ('Abemaciclib', 'Abemaciclib', 'Capsule', '100 mg', 'Verzenio', 'BATCH132', '2015-01-31T00:00:00Z', 2700.00, 1110),
        ('Palbociclib', 'Palbociclib', 'Capsule', '125 mg', 'Ibrance', 'BATCH133', '2014-12-31T00:00:00Z', 2800.00, 1120),
        ('Ribociclib', 'Ribociclib', 'Tablet', '200 mg', 'Kisqali', 'BATCH134', '2014-11-30T00:00:00Z', 2900.00, 1130),
        ('Osimertinib', 'Osimertinib', 'Tablet', '80 mg', 'Tagrisso', 'BATCH135', '2014-10-31T00:00:00Z', 3000.00, 1140),
        ('Dabrafenib', 'Dabrafenib', 'Capsule', '75 mg', 'Tafinlar', 'BATCH136', '2014-09-30T00:00:00Z', 3100.00, 1150),
        ('Trametinib', 'Trametinib', 'Tablet', '2 mg', 'Mekinist', 'BATCH137', '2014-08-31T00:00:00Z', 3200.00, 1160),
        ('Vemurafenib', 'Vemurafenib', 'Tablet', '960 mg', 'Zelboraf', 'BATCH138', '2014-07-31T00:00:00Z', 3300.00, 1170),
        ('Cobimetinib', 'Cobimetinib', 'Tablet', '60 mg', 'Cotellic', 'BATCH139', '2014-06-30T00:00:00Z', 3400.00, 1180),
        ('Atezolizumab', 'Atezolizumab', 'Injection', '1200 mg', 'Tecentriq', 'BATCH140', '2014-05-31T00:00:00Z', 3500.00, 1190),
        ('Durvalumab', 'Durvalumab', 'Injection', '1500 mg', 'Imfinzi', 'BATCH141', '2014-04-30T00:00:00Z', 3600.00, 1200),
        ('Nivolumab', 'Nivolumab', 'Injection', '240 mg', 'Opdivo', 'BATCH142', '2014-03-31T00:00:00Z', 3700.00, 1210),
        ('Pembrolizumab', 'Pembrolizumab', 'Injection', '200 mg', 'Keytruda', 'BATCH143', '2014-02-28T00:00:00Z', 3800.00, 1220),
        ('Atezolizumab', 'Atezolizumab', 'Injection', '840 mg', 'Tecentriq', 'BATCH144', '2014-01-31T00:00:00Z', 3900.00, 1230),
        ('Brentuximab Vedotin', 'Brentuximab Vedotin', 'Injection', '50 mg', 'Adcetris', 'BATCH145', '2013-12-31T00:00:00Z', 4000.00, 1240),
        ('Inotuzumab Ozogamicin', 'Inotuzumab Ozogamicin', 'Injection', '1 mg', 'Besponsa', 'BATCH146', '2013-11-30T00:00:00Z', 4100.00, 1250),
        ('Polatuzumab Vedotin', 'Polatuzumab Vedotin', 'Injection', '1.8 mg', 'Polivy', 'BATCH147', '2013-10-31T00:00:00Z', 4200.00, 1260),
        ('Tisagenlecleucel', 'Tisagenlecleucel', 'Injection', '1 x 10^6 cells', 'Kymriah', 'BATCH148', '2013-09-30T00:00:00Z', 4300.00, 1270),
        ('Axicabtagene Ciloleucel', 'Axicabtagene Ciloleucel', 'Injection', '1 x 10^ 6 cells', 'Yescarta', 'BATCH149', '2013-08-31T00:00:00Z', 4400.00, 1280),
        ('Blinatumomab', 'Blinatumomab', 'Injection', '38 mcg', 'Blincyto', 'BATCH150', '2013-07-31T00:00:00Z', 4500.00, 1290),
        ('Chimeric Antigen Receptor T-Cell Therapy', 'CAR-T', 'Injection', '1 x 10^6 cells', 'Kymriah', 'BATCH151', '2013-06-30T00:00:00Z', 4600.00, 1300),
        ('Nivolumab', 'Nivolumab', 'Injection', '40 mg', 'Opdivo', 'BATCH152', '2013-05-31T00:00:00Z', 4700.00, 1310),
        ('Pembrolizumab', 'Pembrolizumab', 'Injection', '50 mg', 'Keytruda', 'BATCH153', '2013-04-30T00:00:00Z', 4800.00, 1320),
        ('Atezolizumab', 'Atezolizumab', 'Injection', '1200 mg', 'Tecentriq', 'BATCH154', '2013-03-31T00:00:00Z', 4900.00, 1330),
        ('Durvalumab', 'Durvalumab', 'Injection', '1500 mg', 'Imfinzi', 'BATCH155', '2013-02-28T00:00:00Z', 5000.00, 1340),
        ('Brentuximab Vedotin', 'Brentuximab Vedotin', 'Injection', '50 mg', 'Adcetris', 'BATCH156', '2013-01-31T00:00:00Z', 5100.00, 1350),
        ('Inotuzumab Ozogamicin', 'Inotuzumab Ozogamicin', 'Injection', '1 mg', 'Besponsa', 'BATCH157', '2012-12-31T00:00:00Z', 5200.00, 1360),
        ('Polatuzumab Vedotin', 'Polatuzumab Vedotin', 'Injection', '1.8 mg', 'Polivy', 'BATCH158', '2012-11-30T00:00:00Z', 5300.00, 1370),
        ('Tisagenlecleucel', 'Tisagenlecleucel', 'Injection', '1 x 10^6 cells', 'Kymriah', 'BATCH159', '2012-10-31T00:00:00Z', 5400.00, 1380),
        ('Axicabtagene Ciloleucel', 'Axicabtagene Ciloleucel', 'Injection', '1 x 10^6 cells', 'Yescarta', 'BATCH160', '2012-09-30T00:00:00Z', 5500.00, 1390),
        ('Blinatumomab', 'Blinatumomab', 'Injection', '38 mcg', 'Blincyto', 'BATCH161', '2012-08-31T00:00:00Z', 5600.00, 1400),
        ('Chimeric Antigen Receptor T-Cell Therapy', 'CAR-T', 'Injection', '1 x 10^6 cells', 'Kymriah', 'BATCH162', '2012-07-31T00:00:00Z', 5700.00, 1410),
        ('Nivolumab', 'Nivolumab', 'Injection', '40 mg', 'Opdivo', 'BATCH163', '2012-06-30T00:00:00Z', 5800.00, 1420),
        ('Pembrolizumab', 'Pembrolizumab', 'Injection', '50 mg', 'Keytruda', 'BATCH164', '2012-05-31T00:00:00Z', 5900.00, 1430),
        ('Atezolizumab', 'Atezolizumab', 'Injection', '1200 mg', 'Tecentriq', 'BATCH165', '2012-04-30T00:00:00Z', 6000.00, 1440),
        ('Durvalumab', 'Durvalumab', 'Injection', '1500 mg', 'Imfin zi', 'BATCH166', '2012-03-31T00:00:00Z', 6100.00, 1450),
        ('Brentuximab Vedotin', 'Brentuximab Vedotin', 'Injection', '50 mg', 'Adcetris', 'BATCH167', '2012-02-29T00:00:00Z', 6200.00, 1460),
        ('Inotuzumab Ozogamicin', 'Inotuzumab Ozogamicin', 'Injection', '1 mg', 'Besponsa', 'BATCH168', '2012-01-31T00:00:00Z', 6300.00, 1470),
        ('Polatuzumab Vedotin', 'Polatuzumab Vedotin', 'Injection', '1.8 mg', 'Polivy', 'BATCH169', '2011-12-31T00:00:00Z', 6400.00, 1480),
        ('Tisagenlecleucel', 'Tisagenlecleucel', 'Injection', '1 x 10^6 cells', 'Kymriah', 'BATCH170', '2011-11-30T00:00:00Z', 6500.00, 1490),
        ('Axicabtagene Ciloleucel', 'Axicabtagene Ciloleucel', 'Injection', '1 x 10^6 cells', 'Yescarta', 'BATCH171', '2011-10-31T00:00:00Z', 6600.00, 1500),
        ('Blinatumomab', 'Blinatumomab', 'Injection', '38 mcg', 'Blincyto', 'BATCH172', '2011-09-30T00:00:00Z', 6700.00, 1510),
        ('Chimeric Antigen Receptor T-Cell Therapy', 'CAR-T', 'Injection', '1 x 10^6 cells', 'Kymriah', 'BATCH173', '2011-08-31T00:00:00Z', 6800.00, 1520),
        ('Nivolumab', 'Nivolumab', 'Injection', '40 mg', 'Opdivo', 'BATCH174', '2011-07-31T00:00:00Z', 6900.00, 1530),
        ('Pembrolizumab', 'Pembrolizumab', 'Injection', '50 mg', 'Keytruda', 'BATCH175', '2011-06-30T00:00:00Z', 7000.00, 1540),
        ('Atezolizumab', 'Atezolizumab', 'Injection', '1200 mg', 'Tecentriq', 'BATCH176', '2011-05-31T00:00:00Z', 7100.00, 1550),
        ('Durvalumab', 'Durvalumab', 'Injection', '1500 mg', 'Imfinzi', 'BATCH177', '2011-04-30T00:00:00Z', 7200.00, 1560),
        ('Brentuximab Vedotin', 'Brentuximab Vedotin', 'Injection', '50 mg', 'Adcetris', 'BATCH178', '2011-03-31T00:00:00Z', 7300.00, 1570),
        ('Inotuzumab Ozogamicin', 'Inotuzumab Ozogamicin', 'Injection', '1 mg', 'Besponsa', 'BATCH179', '2011-02-28T00:00:00Z', 7400.00, 1580),
        ('Polatuzumab Vedotin', 'Polatuzumab Vedotin', 'Injection', '1.8 mg', 'Polivy', 'BATCH180', '2011-01-31T00:00:00Z', 7500.00, 1590)
) AS source ([MedicineName], [GenericName], [DosageForm], [Strength], [Manufacturer], [BatchNumber], [ExpiryDate], [PricePerUnit], [StockQuantity])
ON target.[MedicineName] = source.[MedicineName]
WHEN NOT MATCHED THEN
    INSERT ([MedicineName], [GenericName], [DosageForm], [Strength], [Manufacturer], [BatchNumber], [ExpiryDate], [PricePerUnit], [StockQuantity], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[MedicineName], source.[GenericName], source.[DosageForm], source.[Strength], source.[Manufacturer], source.[BatchNumber], source.[ExpiryDate], source.[PricePerUnit], source.[StockQuantity], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[GenericName] = source.[GenericName],
        target.[DosageForm] = source.[DosageForm],
        target.[Strength] = source.[Strength],
        target.[Manufacturer] = source.[Manufacturer],
        target.[BatchNumber] = source.[BatchNumber],
        target.[ExpiryDate] = source.[ExpiryDate],
        target.[PricePerUnit] = source.[PricePerUnit],
        target.[StockQuantity] = source.[StockQuantity],
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;