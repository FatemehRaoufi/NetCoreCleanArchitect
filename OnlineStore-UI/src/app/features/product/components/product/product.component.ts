// src/app/features/product/components/product.component.ts

import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../product.service';
import { Product } from 'src/app/core/models/product.model'; // Assuming you have a shared model for Product

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.productService.getProducts().subscribe(
      (data: Product[]) => {
        this.products = data;
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }
}
