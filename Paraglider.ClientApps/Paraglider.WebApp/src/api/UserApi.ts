import { AxiosPromise } from "axios";

import { User } from "../typings/server";
import { axiosClient } from "./Api";

export interface IUserApi {
  getUser: () => AxiosPromise<User>;
}

export class UserApi implements IUserApi {
  private readonly baseUrl = "api/v1/user";

  public getUser() {
    const url = `${this.baseUrl}`;
    return axiosClient.get<User>(url);
  }
}
