import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from 'src/app/core/features/product/product-list/product-list.component';
import { ProductService } from './product.service';

@NgModule({
  declarations: [ProductListComponent], // Declare the components
  imports: [CommonModule], // Import any other modules
  providers: [ProductService], // Register services related to this module
  exports: [ProductListComponent], // Export if you want to use this component in other modules
})
export class ProductModule { }
