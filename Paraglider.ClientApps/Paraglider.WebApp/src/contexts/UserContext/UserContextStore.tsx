import { PropsWithChildren, useCallback, useContext, useEffect, useState } from "react";

import { User } from "../../typings/server";
import { ApiContext } from "../ApiContext";
import { UserContext } from "./UserContext";

export const UserContextStore = ({ children }: PropsWithChildren) => {
  const { userApi } = useContext(ApiContext);
  const [user, setUser] = useState<User>();

  const fetchUser = useCallback(async () => {
    try {
      const response = await userApi.getUser();
      setUser(response.data);
    } catch (e) {
      console.error(e);
    }
  }, []);

  useEffect(() => {
    fetchUser();
  }, [fetchUser]);

  return (
    <UserContext.Provider value={{ user, fetchUser, setUser }}>{children}</UserContext.Provider>
  );
};
