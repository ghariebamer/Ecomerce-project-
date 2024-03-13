import { Injectable } from '@angular/core';
import { environment } from '../enviorment';
import { HttpClient } from '@angular/common/http';
import { Category } from '../interfaces/Models/Category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
baseUrl=environment.CategoryUrl;
  constructor(private Http:HttpClient) { }

  getAllCategories(){
    return this.Http.get<Category[]>(this.baseUrl+'/GetAllGategories')
  }
}
