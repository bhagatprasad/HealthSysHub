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
MERGE INTO [dbo].[PaymentType] AS target
USING (
    VALUES 
        ('Cash', 'Payment made using physical currency.'),
        ('Credit Card', 'Payment made using a credit card issued by a financial institution.'),
        ('Debit Card', 'Payment made using a debit card linked to a bank account.'),
        ('Insurance', 'Payment made through health insurance coverage.'),
        ('Check', 'Payment made using a written check.'),
        ('Electronic Funds Transfer (EFT)', 'Payment made electronically from one bank account to another.'),
        ('Mobile Payment', 'Payment made using mobile payment applications or services.'),
        ('Payment Plan', 'A structured payment arrangement over time for medical services.'),
        ('Health Savings Account (HSA)', 'Payment made using funds from a health savings account.'),
        ('Flexible Spending Account (FSA)', 'Payment made using pre-tax dollars from a flexible spending account.'),
        ('Third-Party Payer', 'Payment made by a third-party organization on behalf of the patient.'),
        ('Charity Care', 'Payment or assistance provided by charitable organizations for medical expenses.'),
        ('Government Assistance', 'Payment made through government programs such as Medicaid or Medicare.'),
        ('Gift Card', 'Payment made using a prepaid gift card.'),
        ('Cryptocurrency', 'Payment made using digital currencies like Bitcoin or Ethereum.'),
        ('Barter', 'Payment made through the exchange of goods or services instead of money.'),
        ('Installment Payment', 'Payment made in multiple scheduled payments over time.'),
        ('Prepayment', 'Payment made in advance for services to be rendered in the future.'),
        ('Co-Payment', 'A fixed amount paid by the patient at the time of service, typically for insurance-covered services.'),
        ('Deductible', 'The amount a patient must pay out-of-pocket before insurance coverage begins.')
) AS source ([PaymentTypeName], [Description])
ON target.[PaymentTypeName] = source.[PaymentTypeName]
WHEN NOT MATCHED THEN
    INSERT ([PaymentTypeName], [Description], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[PaymentTypeName], source.[Description], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[Description] = source.[Description],
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;