import { CommonModule } from '@angular/common';
import { Component, EventEmitter, input, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PharmacyStaff } from '../models/pharmacystaff';

@Component({
  selector: 'app-create',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent implements OnChanges {
  pharmacyStaffForm: FormGroup;

  @Input() isVisible: boolean = false;

  private _pharmacyStaff: PharmacyStaff | null = null;

  @Input()
  set pharmacyStaff(value: PharmacyStaff | null) {
    this._pharmacyStaff = value;
    if (value) {
      this.patchForm(value);
    }
  }
  get pharmacyStaff(): PharmacyStaff | null {
    return this._pharmacyStaff;
  }
  @Output() closeSidebar = new EventEmitter<void>();
  @Output() savePharmacyStaff = new EventEmitter<PharmacyStaff>();


  constructor(private fb: FormBuilder) {
    this.pharmacyStaffForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      designation: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', Validators.required],
      isActive: [true]
    });

  }
  private patchForm(pharmacyStaff: PharmacyStaff): void {
    const formattedData = {
      ...pharmacyStaff
    };
    console.log('Patching form with:', formattedData);
    this.pharmacyStaffForm.patchValue(formattedData);
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['pharmacyStaff'] && changes['pharmacyStaff'].currentValue) {
      this.patchForm(changes['pharmacyStaff'].currentValue);
    }
  }
  close(): void {
    this.closeSidebar.emit();
  }
  OnSubmit(): void {
    if (this.pharmacyStaffForm.valid) {
      const staffData: PharmacyStaff = {
        ...this.pharmacyStaffForm.value,
        staffId: this.pharmacyStaff?.staffId,
        pharmacyId: this.pharmacyStaff?.pharmacyId,
        hospitalId: this.pharmacyStaff?.hospitalId
      };

      console.log(staffData);
      this.savePharmacyStaff.emit(staffData);
    }
  }
}
