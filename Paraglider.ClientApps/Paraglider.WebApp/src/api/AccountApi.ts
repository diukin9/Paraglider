import axios, { AxiosResponse } from "axios";

import { RegisterUserRequest } from "../typings/server";

export interface IAccountAPi {
  register: (data: RegisterUserRequest) => Promise<AxiosResponse<null, null>>;
}

export class AccountApi implements IAccountAPi {
  private readonly baseUrl = "api/v1/account";

  public register(data: RegisterUserRequest) {
    const url = `${this.baseUrl}/register`;
    return axios.post(url, data);
  }
}
