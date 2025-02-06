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
MERGE INTO [dbo].[PatientType] AS target
USING (
    VALUES 
        ('Inpatient', 'Patients who are admitted to the hospital for at least one overnight stay.'),
        ('Outpatient', 'Patients who receive care without being admitted to the hospital.'),
        ('Emergency', 'Patients who require immediate medical attention due to a critical condition.'),
        ('Observation', 'Patients who are placed under observation for a short period to determine the need for admission.'),
        ('Same-Day Surgery', 'Patients who undergo surgical procedures and are discharged on the same day.'),
        ('Pediatric', 'Patients who are children or adolescents, typically under the age of 18.'),
        ('Geriatric', 'Patients who are elderly, typically aged 65 and older.'),
        ('Maternity', 'Patients who are pregnant or have recently given birth.'),
        ('Rehabilitation', 'Patients who are receiving therapy to recover from illness or injury.'),
        ('Chronic Care', 'Patients with long-term health conditions requiring ongoing management.'),
        ('Palliative Care', 'Patients receiving care focused on providing relief from symptoms of serious illness.'),
        ('Home Health', 'Patients receiving medical care in their own homes.'),
        ('Telehealth', 'Patients receiving care through virtual consultations and remote monitoring.'),
        ('Long-Term Care', 'Patients who require extended care in facilities such as nursing homes.'),
        ('Mental Health', 'Patients receiving treatment for mental health conditions.'),
        ('Substance Abuse', 'Patients undergoing treatment for substance use disorders.'),
        ('Preventive Care', 'Patients receiving care aimed at preventing diseases and maintaining health.'),
        ('Urgent Care', 'Patients who need immediate care for non-life-threatening conditions.'),
        ('Surgical', 'Patients who are undergoing surgical procedures.'),
        ('Diagnostic', 'Patients undergoing tests and evaluations to diagnose health conditions.'),
        ('Follow-Up', 'Patients returning for follow-up care after treatment or surgery.')
) AS source ([PatientTypeName], [Description])
ON target.[PatientTypeName] = source.[PatientTypeName]
WHEN NOT MATCHED THEN
    INSERT ([PatientTypeName], [Description], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[PatientTypeName], source.[Description], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[Description] = source.[Description],
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;