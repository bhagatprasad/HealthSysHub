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
MERGE INTO [dbo].[Department] AS target
USING (
    VALUES 
        ('Cardiology', 'Department specializing in heart and blood vessel disorders'),
        ('Pediatrics', 'Department focused on medical care for infants children and adolescents'),
        ('Dermatology', 'Department dealing with skin hair and nail disorders'),
        ('Orthopedics', 'Department specializing in musculoskeletal system disorders'),
        ('Neurology', 'Department focused on nervous system disorders'),
        ('Oncology', 'Department specializing in cancer treatment and research'),
        ('Gynecology', 'Department dealing with female reproductive system disorders'),
        ('Psychiatry', 'Department focused on mental health disorders'),
        ('Endocrinology', 'Department specializing in hormonal and metabolic disorders'),
        ('Urology', 'Department dealing with urinary tract and male reproductive system disorders'),
        ('Radiology', 'Department specializing in medical imaging and diagnosis'),
        ('Gastroenterology', 'Department focused on digestive system disorders'),
        ('Pulmonology', 'Department specializing in respiratory system disorders'),
        ('Nephrology', 'Department dealing with kidney disorders'),
        ('Hematology', 'Department focused on blood disorders'),
        ( 'Allergy and Immunology', 'Department specializing in allergic conditions and immune system disorders'),
        ('Infectious Disease', 'Department focused on infections and diseases caused by pathogens'),
        ( 'Rheumatology', 'Department dealing with joint and connective tissue disorders'),
        ('Physical Medicine', 'Department focused on rehabilitation and physical therapy'),
        ('Emergency Medicine', 'Department providing immediate medical care for acute conditions'),
        ('Anesthesiology', 'Department specializing in pain management and anesthesia'),
        ('Pathology', 'Department focused on disease diagnosis through laboratory analysis'),
        ('Ophthalmology', 'Department dealing with eye and vision disorders'),
        ('Otolaryngology', 'Department specializing in ear nose and throat disorders'),
        ('Family Medicine', 'Department providing comprehensive health care for individuals and families'),
        ('Internal Medicine', 'Department focused on adult diseases and general health care'),
        ('Plastic Surgery', 'Department specializing in reconstructive and cosmetic surgery'),
        ('Sports Medicine', 'Department dealing with injuries related to sports and exercise'),
        ('Geriatrics', 'Department focused on health care for elderly patients'),
        ('Palliative Care', 'Department providing supportive care for serious illnesses'),
        ('Addiction Medicine', 'Department specializing in treatment of addiction and substance use disorders'),
        ('Genetics', 'Department focused on genetic disorders and counseling'),
        ('Preventive Medicine', 'Department specializing in health promotion and disease prevention'),
        ('Occupational Medicine', 'Department dealing with workplace-related health issues'),
        ('Sleep Medicine', 'Department specializing in sleep disorders and treatments'),
        ('Nuclear Medicine', 'Department using radioactive substances for diagnosis and treatment'),
        ('Interventional Radiology', 'Department specializing in minimally invasive procedures using imaging guidance'),
        ('Vascular Surgery', 'Department dealing with surgical treatment of blood vessel disorders'),
        ('Thoracic Surgery', 'Department specializing in surgery of the chest including the heart and lungs'),
        ('Colorectal Surgery', 'Department dealing with surgery of the colon rectum and anus'),
        ('Bariatric Surgery', 'Department specializing in weight loss surgery'),
        ('Transplant Surgery', 'Department focused on organ transplantation procedures'),
        ('Endocrine Surgery', 'Department specializing in surgery of the endocrine glands'),
        ('Pediatric Surgery', 'Department providing surgical care for children'),
        ('Trauma Surgery', 'Department specializing in surgical treatment of traumatic injuries'),
        ('Cardiothoracic Surgery', 'Department dealing with surgery of the heart and chest'),
        ('Neurosurgery', 'Department specializing in surgery of the nervous system'),
        ('Reproductive Endocrinology', 'Department focused on hormonal disorders and reproductive health'),
        ('Maternal-Fetal Medicine', 'Department specializing in high-risk pregnancies'),
        ('Pain Management', 'Department focused on alleviating chronic pain conditions'),
        ('Wound Care', 'Department specializing in the treatment of chronic and complex wounds'),
        ('Nutrition', 'Department providing dietary and nutritional guidance'),
        ('Health Education', 'Department focused on educating patients about health and wellness'),
        ('Clinical Research', 'Department conducting research to improve patient care and treatment outcomes')
) AS source ([DepartmentName], [DepartmentDescription])
ON target.[DepartmentName] = source.[DepartmentName]
WHEN NOT MATCHED THEN
    INSERT ([DepartmentName], [DepartmentDescription], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[DepartmentName], source.[DepartmentDescription], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[DepartmentDescription] = source.[DepartmentDescription],
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;