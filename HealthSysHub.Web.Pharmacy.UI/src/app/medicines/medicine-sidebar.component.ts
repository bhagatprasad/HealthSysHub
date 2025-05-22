import { Component, EventEmitter, Input, Output, SimpleChanges, OnChanges } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PharmacyMedicine } from '../models/pharmacymedicine';
import { CommonModule } from '@angular/common';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-medicine-sidebar',
  templateUrl: './medicine-sidebar.component.html',
  styleUrls: ['./medicine-sidebar.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  providers: [DatePipe]
})
export class MedicineSidebarComponent implements OnChanges {
  @Input() isVisible: boolean = false;
  private _medicine: PharmacyMedicine | null = null;

  @Input()
  set medicine(value: PharmacyMedicine | null) {
    console.log('Medicine input received:', value);
    this._medicine = value;
    if (value) {
      this.patchForm(value);
    }
  }
  get medicine(): PharmacyMedicine | null {
    return this._medicine;
  }

  @Output() closeSidebar = new EventEmitter<void>();
  @Output() saveMedicine = new EventEmitter<PharmacyMedicine>();

  medicineForm: FormGroup;

  constructor(private fb: FormBuilder, private datePipe: DatePipe) {
    this.medicineForm = this.fb.group({
      medicineName: ['', Validators.required],
      genericName: [''],
      dosageForm: [''],
      strength: [''],
      manufacturer: [''],
      batchNumber: [''],
      expiryDate: [''],
      pricePerUnit: [0, [Validators.required, Validators.min(0)]],
      stockQuantity: [0, [Validators.required, Validators.min(0)]],
      isActive: [true]
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['medicine'] && changes['medicine'].currentValue) {
      this.patchForm(changes['medicine'].currentValue);
    }
  }

  private patchForm(medicine: PharmacyMedicine): void {
    const formattedData = {
      ...medicine,
      expiryDate: this.formatDate(medicine.expiryDate)
    };
    console.log('Patching form with:', formattedData);
    this.medicineForm.patchValue(formattedData);
  }

  private formatDate(date: any): string | null {
    if (!date) return null;
    if (date instanceof Date) {
      return this.datePipe.transform(date, 'yyyy-MM-dd');
    }
    if (typeof date === 'string') {
      const parsedDate = new Date(date);
      return this.datePipe.transform(parsedDate, 'yyyy-MM-dd');
    }
    return null;
  }

  close(): void {
    this.closeSidebar.emit();
  }

  onSubmit(): void {
    if (this.medicineForm.valid) {
      const medicineData: PharmacyMedicine = {
        ...this.medicineForm.value,
        medicineId: this.medicine?.medicineId || ''  // Include the ID if it exists
      };
      this.saveMedicine.emit(medicineData);
    }
  }
}