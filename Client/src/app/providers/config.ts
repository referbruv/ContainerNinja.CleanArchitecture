import { environment } from "src/environments/environment";

export class Config {
    static api: string = environment.apiBaseUri; //"https://localhost:5001/api/v1";
}