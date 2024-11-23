import { Component, inject } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filter-dialog',
  imports: [FormsModule],
  templateUrl: './filter-dialog.component.html',
  styleUrl: './filter-dialog.component.css'
})
export class FilterDialogComponent {
  shopService = inject(ShopService);
  private dialogRef = inject(MatDialogRef<FilterDialogComponent>);

  data = inject(MAT_DIALOG_DATA);

  selectedBrands: string[] = this.data.selectedBrands;
  selectedTypes: string[] = this.data.selectedTypes;

  onBrandSelect(brand: string, event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;

    if (isChecked) {
      this.selectedBrands.push(brand);
      console.log(brand);
    } else {
      this.selectedBrands = this.selectedBrands.filter(t => t !== brand);
    }
  }
  onTypeSelect(type: string, event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;

    if (isChecked) {
      this.selectedTypes.push(type);
      console.log(type);
    } else {
      this.selectedTypes = this.selectedTypes.filter(t => t !== type);
    }
  }

  applyFilters() {
    this.dialogRef.close({
    //   selectedBrands: this.selectedBrands,
    //   selectedTypes: this.selectedTypes
    });
    console.log("Brands: " + this.selectedBrands);
    console.log("Types: " + this.selectedTypes);
  }
}
