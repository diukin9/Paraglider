import { PropsWithChildren } from "react";

import { AccountApi, AuthApi, CitiesApi, IApi, UserApi } from "../../api";
import { ApiContext } from "./ApiContext";

export const ApiContextStore = ({ children }: PropsWithChildren) => {
  const api: IApi = {
    accountApi: new AccountApi(),
    authApi: new AuthApi(),
    citiesApi: new CitiesApi(),
    userApi: new UserApi(),
  };

  return <ApiContext.Provider value={api}>{children}</ApiContext.Provider>;
};
