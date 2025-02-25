﻿MERGE INTO [dbo].[LabTest] AS target
USING (
    VALUES 
        ('Complete Blood Count', 'A test that evaluates overall health and detects a variety of disorders, such as anemia and infection.'),
        ('Basic Metabolic Panel', 'A group of tests that measure glucose, calcium, and electrolytes to assess metabolism and kidney function.'),
        ('Comprehensive Metabolic Panel', 'A broader test that includes all tests in the basic metabolic panel plus additional tests for liver function and protein levels.'),
        ('Lipid Panel', 'A test that measures cholesterol levels and triglycerides to assess cardiovascular risk.'),
        ('Thyroid Stimulating Hormone (TSH)', 'A test that measures the level of TSH to evaluate thyroid function.'),
        ('Hemoglobin A1c', 'A test that measures average blood sugar levels over the past 2 to 3 months to assess diabetes control.'),
        ('Urinalysis', 'A test that examines urine for signs of kidney disease, diabetes, and urinary tract infections.'),
        ('Coagulation Panel', 'A group of tests that assess blood clotting function and risk of bleeding.'),
        ('Liver Function Tests', 'A group of tests that evaluate the health of the liver by measuring levels of proteins, liver enzymes, and bilirubin.'),
        ('Kidney Function Tests', 'Tests that assess how well the kidneys are working, including creatinine and blood urea nitrogen (BUN) tests.'),
        ('Vitamin D Test', 'A test that measures the level of vitamin D in the blood to assess bone health and immune function.'),
        ('Prostate-Specific Antigen (PSA)', 'A test that measures the level of PSA in the blood to screen for prostate cancer.'),
        ('C-Reactive Protein (CRP)', 'A test that measures the level of CRP in the blood to assess inflammation.'),
        ('B-type Natriuretic Peptide (BNP)', 'A test that measures the level of BNP to evaluate heart failure.'),
        ('HIV Test', 'A test that detects the presence of HIV antibodies or antigens in the blood.'),
        ('Hepatitis Panel', 'A group of tests that detect hepatitis A, B, and C infections.'),
        ('Blood Culture', 'A test that detects the presence of bacteria or fungi in the blood.'),
        ('Stool Culture', 'A test that detects pathogens in the stool to diagnose gastrointestinal infections.'),
        ('Allergy Testing', 'Tests that identify specific allergens that may cause allergic reactions.'),
        ('Genetic Testing', 'Tests that analyze DNA to identify genetic disorders or predispositions.'),
        ('Pregnancy Test', 'A test that detects the presence of human chorionic gonadotropin (hCG) in the blood or urine.'),
        (' Urine Culture', 'A test that detects bacteria in the urine to diagnose urinary tract infections.'),
        ('Electrolyte Panel', 'A test that measures levels of electrolytes in the blood, including sodium, potassium, and chloride.'),
        ('Creatinine Test', 'A test that measures the level of creatinine in the blood to assess kidney function.'),
        ('Lactate Test', 'A test that measures the level of lactate in the blood to assess tissue oxygenation.'),
        ('Ammonia Test', 'A test that measures the level of ammonia in the blood to assess liver function.'),
        ('Arterial Blood Gas (ABG)', 'A test that measures oxygen and carbon dioxide levels in the blood to assess lung function.'),
        ('D-dimer Test', 'A test that measures the level of D-dimer in the blood to assess for blood clots.'),
        ('Thyroid Panel', 'A group of tests that evaluate thyroid function, including TSH, T3, and T4 levels.'),
        ('Fasting Blood Sugar', 'A test that measures blood sugar levels after fasting to assess diabetes risk.'),
        ('Insulin Test', 'A test that measures the level of insulin in the blood to assess insulin resistance.'),
        ('Cortisol Test', 'A test that measures the level of cortisol in the blood to assess adrenal function.'),
        ('Lipid Profile', 'A test that measures levels of different types of cholesterol and triglycerides in the blood.'),
        ('Bone Density Test', 'A test that measures bone mineral density to assess osteoporosis risk.'),
        ('Skin Test for Tuberculosis', 'A test that checks for exposure to tuberculosis bacteria.'),
        ('Rapid Strep Test', 'A test that quickly detects streptococcal bacteria in the throat.'),
        ('COVID-19 Test', 'A test that detects the presence of the virus that causes COVID-19, typically through a nasal swab or saliva sample.'),
        ('Hepatic Function Panel', 'A group of tests that assess liver function and health.'),
        ('Serum Protein Electrophoresis', 'A test that measures specific proteins in the blood to diagnose various conditions.'),
        ('Vitamin B12 Test', 'A test that measures the level of vitamin B12 in the blood to assess for deficiency.'),
        ('Folate Test', 'A test that measures the level of folate in the blood to assess for deficiency.'),
        ('Antibody Testing', 'Tests that detect antibodies in the blood to assess immune response to infections or vaccines.'),
        ('Metanephrines Test', 'A test that measures levels of metanephrines to evaluate for pheochromocytoma.'),
        ('Heavy Metals Test', 'A test that measures levels of heavy metals in the blood to assess for toxicity.'),
        ('Thyroid Antibodies Test', 'A test that detects antibodies against thyroid tissue to evaluate autoimmune thyroid disease.'),
        ('Sputum Culture', 'A test that detects pathogens in sputum to diagnose respiratory infections.'),
        ('Urine Drug Screen', 'A test that detects the presence of drugs in the urine.'),
        ('Saliva Test', 'A test that analyzes saliva for various substances, including hormones and drugs.'),
        ('Nutritional Assessment', 'A test that evaluates nutritional status through various biomarkers.'),
        ('Microbiology Tests', 'Tests that identify microorganisms in samples to diagnose infections.'),
        ('Tissue Biopsy', 'A procedure that involves taking a small sample of tissue for laboratory analysis.'),
        ('Cytology Test', 'A test that examines cells from various body sites to detect cancer or other conditions.')
) AS source ([TestName], [TestDescription])
ON target.[TestName] = source.[TestName]
WHEN NOT MATCHED THEN
    INSERT ([TestName], [TestDescription], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[TestName], source.[TestDescription], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[TestDescription] = source.[TestDescription],
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;