import axios from "axios";

import { AccountApi } from "./AccountApi";
import { AuthApi } from "./AuthApi";
import { CitiesApi } from "./CitiesApi";
import { UserApi } from "./UserApi";

const getBaseURL = () => {
  const { PROD, VITE_API_PROXY_URL } = import.meta.env;
  return PROD ? document.location.origin : VITE_API_PROXY_URL;
};

export const axiosClient = axios.create({
  baseURL: getBaseURL(),
  withCredentials: true,
});

export interface IApi {
  accountApi: AccountApi;
  authApi: AuthApi;
  citiesApi: CitiesApi;
  userApi: UserApi;
}
