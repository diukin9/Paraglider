import { PropsWithChildren, useCallback, useContext, useEffect, useState } from "react";

import { User } from "../../typings/server";
import { ApiContext } from "../ApiContext";
import { UserContext } from "./UserContext";

export const UserContextStore = ({ children }: PropsWithChildren) => {
  const { userApi } = useContext(ApiContext);
  const [user, setUser] = useState<User>();

  const fetchUser = useCallback(async () => {
    const response = await userApi.getUser();
    setUser(response.data);
  }, []);

  useEffect(() => {
    fetchUser();
  }, [fetchUser]);

  return <UserContext.Provider value={{ user, setUser }}>{children}</UserContext.Provider>;
};
