import { AxiosPromise } from "axios";

import { City } from "../typings/server";
import { axiosClient } from "./Api";

export interface ICitiesApi {
  getCities: () => AxiosPromise<City[]>;
}

export class CitiesApi implements ICitiesApi {
  private readonly baseUrl = "api/v1/cities";

  public getCities() {
    const url = `${this.baseUrl}`;
    return axiosClient.get<City[]>(url);
  }
}
