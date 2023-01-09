import axios from "axios";
import { PropsWithChildren, useCallback, useEffect } from "react";

import { AccountApi, AuthApi, IApi, UserApi } from "../../api";
import { ApiContext } from "./ApiContext";

export const ApiContextStore = ({ children }: PropsWithChildren) => {
  const api: IApi = {
    accountApi: new AccountApi(),
    authApi: new AuthApi(),
    userApi: new UserApi(),
  };

  const setGlobalAxiosConfig = useCallback(() => {
    axios.defaults.baseURL = import.meta.env.VITE_API_PROXY_URL;
    axios.defaults.withCredentials = true;
  }, []);

  useEffect(() => {
    setGlobalAxiosConfig();
  }, [setGlobalAxiosConfig]);

  return <ApiContext.Provider value={api}>{children}</ApiContext.Provider>;
};
