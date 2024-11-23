import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../shared/models/product';
import { ShopService } from '../../core/services/shop.service';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { FilterDialogComponent } from './filter-dialog/filter-dialog.component';
import { concatWith } from 'rxjs';


@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [ProductItemComponent, MatIconModule],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css'
})
export class ShopComponent implements OnInit{
  private shopService = inject(ShopService);
  private dialogService = inject(MatDialog);
  title = 'MiniShop';

  products: Product[] = [];

  // this is for store the selected brands and types...
  selectedBrands: string[] = [];
  selectedTypes: string[] = [];
  searchByBrand: string[] = [];

  // future plan to sort product using custom implementation...
  selectedSort: string = 'name';
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low-High', value: 'priceAsc'},
    {name: 'Price: High-Low', value: 'priceDesc'}
  ];

  initialLoad: boolean = false;


  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop() {
    if (this.initialLoad === false) {
      console.log("Coming here...");
      this.shopService.getBrands();
      this.shopService.getTypes();
      this.shopService.getProducts().subscribe({
        next: response => this.products = response,
        error: error => console.log(error),
        complete: () => console.log("Complete")
      });
    }
    this.initialLoad = true;
  }

  openFilterDialog() {
    const dialogRef = this.dialogService.open(FilterDialogComponent, {
      minWidth: '250px',
      data: {
        selectedBrands: this.selectedBrands,
        selectedTypes: this.selectedTypes
      }
    });

    dialogRef.afterClosed().subscribe({
      next: result => {
        if (this.selectedBrands || this.selectedTypes) {
          // apply filter
          console.log("result selected Brand: " + this.selectedBrands);
          console.log("result selected Types: " + this.selectedTypes);
          
        this.shopService.getProducts(this.selectedBrands, this.selectedTypes).subscribe({
          next: response => this.products = response,
          error: error => console.log(error),
          complete: () => console.log("Complete to fetch from getProduct()")
        });
        }
      }
    })
  }

  searchProduct(searchValue: string): void {
    
    console.log('Search Value:', searchValue);
    this.searchByBrand.push(searchValue);
    // You can now use the searchValue for your logic
    
    this.shopService.getProducts(this.searchByBrand).subscribe({
      next: response => this.products = response,
      error: error => console.log(error),
      complete: () => console.log("Complete to fetch from getProduct()")
    });
  }

}
