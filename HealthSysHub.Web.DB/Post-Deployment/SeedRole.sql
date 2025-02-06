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
MERGE INTO [dbo].[Role] AS target
USING (
    VALUES 
        ('Administrator', 'Admin', 'Responsible for overseeing the entire system and managing user access.'),
        ('Doctor', 'DOC', 'Medical professional responsible for diagnosing and treating patients.'),
        ('Nurse', 'NUR', 'Healthcare professional providing patient care and support.'),
        ('Pharmacist', 'PHARM', 'Responsible for dispensing medications and advising on their safe use.'),
        ('Lab Technician', 'LAB', 'Performs laboratory tests and analyses.'),
        ('Radiologist', 'RAD', 'Specializes in interpreting medical images.'),
        ('Receptionist', 'REC', 'Handles patient scheduling and administrative tasks.'),
        ('Billing Specialist', 'BILL', 'Manages patient billing and insurance claims.'),
        ('IT Support', 'IT', 'Provides technical support and maintenance for healthcare systems.'),
        ('Physical Therapist', 'PT', 'Helps patients improve their physical function and mobility.'),
        ('Occupational Therapist', 'OT', 'Assists patients in improving their ability to perform daily activities.'),
        ('Dietitian', 'DIET', 'Provides nutritional advice and meal planning.'),
        ('Social Worker', 'SW', 'Supports patients and families in navigating healthcare and social services.'),
        ('Case Manager', 'CM', 'Coordinates patient care and services across the healthcare system.'),
        ('Health Educator', 'HE', 'Educates patients and the community about health and wellness.'),
        ('Surgeon', 'SURG', 'Performs surgical procedures to treat injuries and diseases.'),
        ('Anesthesiologist', 'ANES', 'Administers anesthesia and monitors patients during surgery.'),
        ('Emergency Medical Technician', 'EMT', 'Provides emergency medical care and transportation.'),
        ('Clinical Research Coordinator', 'CRC', 'Manages clinical trials and research studies.'),
        ('Quality Assurance Specialist', 'QA', 'Ensures compliance with healthcare regulations and standards.'),
        ('Compliance Officer', 'CO', 'Oversees adherence to laws and regulations in healthcare practices.'),
        ('Facility Manager', 'FM', 'Responsible for the maintenance and operation of healthcare facilities.'),
        ('Volunteer', 'VOL', 'Provides support and assistance in various hospital departments.'),
        ('Patient Advocate', 'PA', 'Represents and supports patients’ rights and needs.'),
        ('Medical Assistant', 'MA', 'Assists doctors with patient care and administrative tasks.'),
        ('Surgical Technician', 'ST', 'Assists in surgical operations and maintains sterile environments.'),
        ('Infection Control Specialist', 'ICS', 'Monitors and prevents infections in healthcare settings.'),
        ('Health Information Manager', 'HIM', 'Manages patient health information and medical records.'),
        ('Billing Analyst', 'BA', 'Analyzes billing data and ensures accuracy in claims processing.'),
        ('Telehealth Coordinator', 'THC', 'Facilitates remote patient consultations and telemedicine services.'),
        ('Behavioral Health Specialist', 'BHS', 'Provides mental health support and counseling.'),
        ('Palliative Care Specialist', 'PCS', 'Focuses on providing relief from the symptoms and stress of serious illness.'),
        ('Chiropractor', 'CHIROP', 'Specializes in diagnosing and treating musculoskeletal disorders.'),
        ('Speech Therapist', 'ST', 'Helps patients with speech and communication disorders.'),
        ('Respiratory Therapist', 'RT', 'Provides care for patients with breathing difficulties.'),
        ('Genetic Counselor', 'GC', 'Advises patients on genetic conditions and testing.'),
        ('Clinical Psychologist', 'CP', 'Provides psychological assessment and therapy.'),
        ('Neonatologist', 'NEO', 'Specializes in the care of newborns, especially premature or ill infants.'),
        ('Geriatrician', 'GER', 'Focuses on healthcare for elderly patients.'),
        ('Oncologist', 'ONC', 'Specializes in the treatment of cancer.'),
        ('Cardiologist', 'CARD', 'Specializes in heart and cardiovascular conditions.'),
        ('Endocrinologist', 'ENDO', 'Focuses on hormonal and metabolic disorders.'),
        ('Pulmonologist', 'PULM', 'Specializes in respiratory system disorders.'),
        ('Rheumatologist', 'RHEUM', 'Focuses on joint and connective tissue disorders.'),
        ('Nephrologist', 'NEPH', 'Specializes in kidney disorders.'),
        ('Hematologist', 'HEM', 'Focuses on blood disorders.'),
        ('Dermatologist', 'DERM', 'Specializes in skin conditions.'),
        ('Ophthalmologist', 'OPH', 'Specializes in eye and vision care.'),
        ('Urologist', 'URO', 'Focuses on urinary tract and male reproductive system disorders.'),
        ('Orthopedic Surgeon', 'ORTHO', 'Specializes in the musculoskeletal system.'),
        ('Plastic Surgeon', 'PLAST', 'Performs reconstructive and cosmetic surgeries.'),
        ('Pathologist', 'PATH', 'Diagnoses diseases by examining tissues and fluids.'),
        ('Radiation Oncologist', 'RADONC', 'Uses radiation therapy to treat cancer.'),
        ('Sports Medicine Physician', 'SPORTS', 'Specializes in treating sports-related injuries.'),
        ('Addiction Specialist', 'ADD', 'Focuses on treating substance use disorders.'),
        ('Pain Management Specialist', 'PMS', 'Helps patients manage chronic pain.'),
        ('Wound Care Specialist', 'WCS', 'Focuses on treating chronic and complex wounds.'),
        ('Pediatrician', 'PED', 'Provides healthcare for infants, children, and adolescents.'),
        ('Family Medicine Physician', 'FMP', 'Provides comprehensive healthcare for individuals and families.'),
        ('Internal Medicine Physician', 'IMP', 'Specializes in adult medicine and chronic diseases.'),
        ('Public Health Official', 'PHO', 'Works on health policies and community health initiatives.'),
        ('Health Policy Analyst', 'HPA', 'Analyzes and develops health policies.'),
        ('Medical Writer', 'MW', 'Creates content related to medical and healthcare topics.'),
        ('Clinical Data Manager', 'CDM', 'Manages clinical trial data and ensures its accuracy.'),
        ('Regulatory Affairs Specialist', 'RAS', 'Ensures compliance with regulations in healthcare products.'),
        ('Health IT Specialist', 'HITS', 'Focuses on implementing and managing health information technology.'),
        ('Medical Billing Specialist', 'MBS', 'Handles billing and coding for medical services.'),
        ('Healthcare Consultant', 'HC', 'Advises healthcare organizations on improving efficiency and quality.'),
        ('Clinical Trial Manager', 'CTM', 'Oversees clinical trials and research studies.'),
        ('Healthcare Recruiter', 'HR', 'Specializes in recruiting healthcare professionals.'),
        ('Patient Safety Officer', 'PSO', 'Focuses on improving patient safety and reducing errors.'),
        ('Informatics Nurse', 'IN', 'Utilizes data and technology to improve patient care.'),
        ('Telemedicine Specialist', 'TS', 'Provides remote healthcare services through technology.'),
        ('Health Coach', 'HC', 'Guides individuals in achieving their health and wellness goals.'),
        ('Medical Illustrator', 'MI', 'Creates visual representations of medical concepts and procedures.')
) AS source ([Name], [Code], [Description])
ON target.[Name] = source.[Name]
WHEN NOT MATCHED THEN
    INSERT ([Name], [Code], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[Name], source.[Code], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[Code] = source.[Code],
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;