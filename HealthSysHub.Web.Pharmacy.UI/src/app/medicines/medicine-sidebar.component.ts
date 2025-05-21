import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PharmacyMedicine } from '../models/pharmacymedicine';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-medicine-sidebar',
  templateUrl: './medicine-sidebar.component.html',
  styleUrls: ['./medicine-sidebar.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]
})
export class MedicineSidebarComponent implements OnInit {
  @Input() isVisible: boolean = false;
  @Input() medicine: PharmacyMedicine | null = null;
  @Output() closeSidebar = new EventEmitter<void>();
  @Output() saveMedicine = new EventEmitter<PharmacyMedicine>();

  medicineForm: FormGroup;

  constructor(private fb: FormBuilder) {
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

  ngOnInit(): void {
    if (this.medicine) {
      this.medicineForm.patchValue(this.medicine);
    }
  }

  close(): void {
    this.closeSidebar.emit();
  }

  onSubmit(): void {
    if (this.medicineForm.valid) {
      const medicineData: PharmacyMedicine = {
        ...this.medicine,
        ...this.medicineForm.value
      };
      this.saveMedicine.emit(medicineData);
    }
  }
}