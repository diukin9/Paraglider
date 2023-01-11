import axios from "axios";

import { AccountApi } from "./AccountApi";
import { AuthApi } from "./AuthApi";
import { CitiesApi } from "./CitiesApi";
import { UserApi } from "./UserApi";

export const axiosClient = axios.create({
  baseURL: import.meta.env.VITE_API_PROXY_URL,
  withCredentials: true,
});

export interface IApi {
  accountApi: AccountApi;
  authApi: AuthApi;
  citiesApi: CitiesApi;
  userApi: UserApi;
}
