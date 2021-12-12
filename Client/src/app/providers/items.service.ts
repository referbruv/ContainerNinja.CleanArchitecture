import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs";
import { CreateOrUpdateItemDTO } from "../models/CreateOrUpdateItemDTO";
import { ItemDTO } from "../models/ItemDTO";

@Injectable({
    providedIn: 'root'
})
export class ItemsService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get("https://localhost:5001/api/v1/Items").pipe((map(x => <ItemDTO[]>x)));
    }

    getById(id: number) {
        return this.http.get(`https://localhost:5001/api/v1/Items/${id}`).pipe((map(x => <ItemDTO>x)));
    }

    create(item: CreateOrUpdateItemDTO) {
        return this.http.post(`https://localhost:5001/api/v1/Items`, item).pipe((map(x => <ItemDTO>x)));
    }

    update(id: number, item: CreateOrUpdateItemDTO) {
        return this.http.put(`https://localhost:5001/api/v1/Items/${id}`, item).pipe((map(x => <ItemDTO>x)));
    }

    add(item: CreateOrUpdateItemDTO) {
        return this.http.post('https://localhost:5001/api/v1/Items', item).pipe((map(x => <ItemDTO>x)));
    }

    delete(id: number) {
        return this.http.delete(`https://localhost:5001/api/v1/Items/${id}`);
    }
}