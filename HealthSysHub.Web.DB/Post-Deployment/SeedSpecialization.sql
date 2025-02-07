/*
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
MERGE INTO [dbo].[Specialization] AS target
USING (
    VALUES 
        ('Cardiology', 'Heart and blood vessel disorders'),
        ('Pediatrics', 'Medical care for infants children and adolescents'),
        ('Dermatology', 'Skin hair and nail disorders'),
        ('Orthopedics', 'Musculoskeletal system disorders'),
        ('Neurology', 'Nervous system disorders'),
        ('Oncology', 'Cancer treatment and research'),
        ('Gynecology', 'Female reproductive system disorders'),
        ('Psychiatry', 'Mental health disorders'),
        ('Endocrinology', 'Hormonal and metabolic disorders'),
        ('Urology', 'Urinary tract and male reproductive system disorders'),
        ('Radiology', 'Medical imaging and diagnosis'),
        ('Gastroenterology', 'Digestive system disorders'),
        ('Pulmonology', 'Respiratory system disorders'),
        ('Nephrology', 'Kidney disorders'),
        ('Hematology', 'Blood disorders'),
        ('Allergy and Immunology', 'Allergic conditions and immune system disorders'),
        ('Infectious Disease', 'Infections and diseases caused by pathogens'),
        ('Rheumatology', 'Joint and connective tissue disorders'),
        ('Physical Medicine', 'Rehabilitation and physical therapy'),
        ('Emergency Medicine', 'Immediate medical care for acute conditions'),
        ('Anesthesiology', 'Pain management and anesthesia'),
        ('Pathology', 'Disease diagnosis through laboratory analysis'),
        ('Ophthalmology', 'Eye and vision disorders'),
        ('Otolaryngology', 'Ear nose and throat disorders'),
        ('Family Medicine', 'Comprehensive health care for individuals and families'),
        ('Internal Medicine', 'Adult diseases and general health care'),
        ('Plastic Surgery', 'Reconstructive and cosmetic surgery'),
        ('Sports Medicine', 'Injuries related to sports and exercise'),
        ('Geriatrics', 'Health care for elderly patients'),
        ('Palliative Care', 'Supportive care for serious illnesses'),
        ('Addiction Medicine', 'Treatment of addiction and substance use disorders'),
        ('Genetics', 'Genetic disorders and counseling'),
        ('Preventive Medicine', 'Health promotion and disease prevention'),
        ('Occupational Medicine', 'Workplace-related health issues'),
        ('Sleep Medicine', 'Sleep disorders and treatments'),
        ('Nuclear Medicine', 'Radioactive substances for diagnosis and treatment'),
        ('Interventional Radiology', 'Minimally invasive procedures using imaging guidance'),
        ('Vascular Surgery', 'Surgical treatment of blood vessel disorders'),
        ('Thoracic Surgery', 'Surgery of the chest including the heart and lungs'),
        ('Colorectal Surgery', 'Surgery of the colon rectum and anus'),
        ('Bariatric Surgery', 'Weight loss surgery'),
        ('Transplant Surgery', 'Organ transplantation procedures'),
        ('Endocrine Surgery', 'Surgery of the endocrine glands'),
        ('Pediatric Surgery', 'Surgical care for children'),
        ('Trauma Surgery', 'Surgical treatment of traumatic injuries'),
        ('Cardiothoracic Surgery', 'Surgery of the heart and chest'),
        ('Neurosurgery', 'Surgery of the nervous system'),
        ('Reproductive Endocrinology', 'Hormonal disorders related to reproduction'),
        ('Fertility Medicine', 'Treatment of infertility'),
        ('Pain Management', 'Management of chronic pain conditions'),
        ('Wound Care', 'Treatment of chronic and acute wounds'),
        ('Pediatric Cardiology', 'Heart disorders in children'),
        ('Pediatric Neurology', 'Nervous system disorders in children'),
        ('Pediatric Endocrinology', 'Hormonal disorders in children'),
        ('Pediatric Gastroenterology', 'Digestive system disorders in children'),
        ('Pediatric Hematology', 'Blood disorders in children'),
        ('Pediatric Infectious Disease', 'Infections in children'),
        ('Pediatric Pulmonology', 'Respiratory disorders in children'),
        ('Pediatric Rheumatology', 'Joint and connective tissue disorders in children'),
        ('Pediatric Urology', 'Urinary tract disorders in children'),
        ('Pediatric Allergy and Immunology', 'Allergic conditions in children'),
        ('Pediatric Orthopedics', 'Musculoskeletal disorders in children'),
        ('Pediatric Dermatology', 'Skin disorders in children'),
        ('Pediatric Psychiatry', 'Mental health disorders in children'),
        ('Pediatric Anesthesiology', 'Anesthesia for children'),
        ('Pediatric Radiology', 'Medical imaging for children'),
        ('Pediatric Pathology', 'Disease diagnosis in children'),
        ('Pediatric Emergency Medicine', 'Emergency care for children'),
        ('Pediatric Critical Care', 'Intensive care for critically ill children'),
        ('Pediatric Cardiac Surgery', 'Surgical treatment of heart disorders in children'),
        ('Pediatric Nephrology', 'Kidney disorders in children'),
        ('Pediatric Gastrointestinal Surgery', 'Surgical treatment of digestive disorders in children'),
        ('Pediatric Otolaryngology', 'Ear nose and throat disorders in children'),
        ('Pediatric Plastic Surgery', 'Reconstructive and cosmetic surgery for children'),
        ('Pediatric Sports Medicine', 'Sports related injuries in children'),
        ('Pediatric Sleep Medicine', 'Sleep disorders in children'),
        ('Pediatric Genetics', 'Genetic disorders in children'),
        ('Pediatric Rehabilitation', 'Rehabilitation services for children'),
        ('Pediatric Palliative Care', 'Supportive care for seriously ill children'),
        ('Pediatric Allergy', 'Allergic conditions in children'),
        ('Pediatric Oncology', 'Cancer treatment for children')
) AS source ([SpecializationName], [SpecializationDescription])
ON target.[SpecializationName] = source.[SpecializationName]
WHEN NOT MATCHED THEN
    INSERT ([SpecializationName], [SpecializationDescription], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[SpecializationName], source.[SpecializationDescription], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[SpecializationDescription] = source.[SpecializationDescription],
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;