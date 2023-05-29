import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';

@Injectable({
    providedIn: 'root'
  })

  export class DataService{
    apiUrl = 'https://localhost:5260/api/';
    constructor(private http: HttpClient) { }

    getAllProducts():Observable<Product[]>{
        return this.http.get<Product[]>(`${this.apiUrl}Store/GetAllProducts`);
    }
  }