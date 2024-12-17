// src/app/features/product/components/product-list/product-list.component.ts
import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../product.service';
import { Product } from 'src/app/core/models/product.model';


@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
  standalone: true,  // This makes the component standalone
  //imports: [CommonModule]  // You can also import necessary modules here
})


//------------------

export class ProductListComponent implements OnInit {
  products: Product[] = [];
  errorMessage: string = '';

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe({
      next: (data) => (this.products = data),
      error: (err) => (this.errorMessage = 'Failed to load products.'),
    });
  }
}
