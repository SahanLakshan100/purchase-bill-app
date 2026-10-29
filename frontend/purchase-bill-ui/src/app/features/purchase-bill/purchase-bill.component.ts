import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { map, Observable, startWith } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';
import { PurchaseBillService } from '../../core/services/purchase-bill.service';

@Component({
  selector: 'app-purchase-bill',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatAutocompleteModule, MatButtonModule, MatTableModule, MatIconModule, MatToolbarModule],
  templateUrl: './purchase-bill.component.html',
  styleUrl: './purchase-bill.component.scss'
})
export class PurchaseBillComponent implements OnInit {
  readonly items = ['Mango','Apple','Banana','Orange','Grapes','Kiwi','Strawberry'];
  filteredItems$!: Observable<string[]>;
  locations: any[] = [];
  rows: any[] = [];
  displayedColumns = ['item','batch','standardCost','standardPrice','discountPct','qty','totalCost','totalSelling'];
  form: any;

  constructor(private fb: FormBuilder, public auth: AuthService, private billService: PurchaseBillService) {
    this.form = this.fb.group({
      item: ['', Validators.required],
      batch: ['', Validators.required],
      standardCost: [0, [Validators.required, Validators.min(0)]],
      standardPrice: [0, [Validators.required, Validators.min(0)]],
      discountPct: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      qty: [1, [Validators.required, Validators.min(1)]]
    });
  }

  ngOnInit(): void {
    this.filteredItems$ = this.form.get('item')!.valueChanges.pipe(
      startWith(''),
      map((v: string) => this.items.filter(i => i.toLowerCase().includes((v || '').toLowerCase())))
    );
    this.billService.getLocations().subscribe((locs: any[]) => this.locations = locs);
  }

  get totalCostPreview(): number {
    const c = this.form.value.standardCost || 0;
    const q = this.form.value.qty || 0;
    const d = this.form.value.discountPct || 0;
    return (c * q) - (c * q * d / 100);
  }
  get totalSellingPreview(): number {
    return (this.form.value.standardPrice || 0) * (this.form.value.qty || 0);
  }
  get totalItems(): number { return this.rows.length; }
  get totalQty(): number { return this.rows.reduce((s, r) => s + r.qty, 0); }
  get grandTotal(): number { return this.rows.reduce((s, r) => s + r.totalSelling, 0); }

  add() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    const v = this.form.value;
    const line = {
      itemName: v.item!, batch: v.batch!, standardCost: v.standardCost!, standardPrice: v.standardPrice!, discountPct: v.discountPct!, qty: v.qty!
    };

    this.billService.addLine(line).subscribe({
      next: (res: any) => {
        this.rows = [...this.rows, { ...line, totalCost: res.totalCost, totalSelling: res.totalSelling }];
        this.form.reset({ qty: 1, discountPct: 0, standardCost: 0, standardPrice: 0 });
      },
      error: (err: any) => alert('Failed: ' + err.message)
    });
  }
}