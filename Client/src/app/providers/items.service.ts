import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs";
import { CreateOrUpdateItemDTO } from "../models/CreateOrUpdateItemDTO";
import { ItemDTO } from "../models/ItemDTO";
import { Config } from "./config";

@Injectable({
    providedIn: 'root'
})
export class ItemsService {
    constructor(private http: HttpClient) { }

    get baseUri() {
        return `${Config.api}`;
    }

    getAll() {
        return this.http.get(`${this.baseUri}/Items`).pipe((map(x => <ItemDTO[]>x)));
    }

    getById(id: number) {
        return this.http.get(`${this.baseUri}/Items/${id}`).pipe((map(x => <ItemDTO>x)));
    }

    create(item: CreateOrUpdateItemDTO) {
        return this.http.post(`${this.baseUri}/Items`, item).pipe((map(x => <ItemDTO>x)));
    }

    update(id: number, item: CreateOrUpdateItemDTO) {
        return this.http.put(`${this.baseUri}/Items/${id}`, item).pipe((map(x => <ItemDTO>x)));
    }

    add(item: CreateOrUpdateItemDTO) {
        return this.http.post(`${this.baseUri}/Items`, item).pipe((map(x => <ItemDTO>x)));
    }

    delete(id: number) {
        return this.http.delete(`${this.baseUri}/Items/${id}`);
    }
}