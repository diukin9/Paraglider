import { PropsWithChildren } from "react";

import { ApiContextStore } from "./ApiContext";
import { UserContextStore } from "./UserContext/UserContextStore";

export const ContextStore = ({ children }: PropsWithChildren) => {
  return (
    <ApiContextStore>
      <UserContextStore>{children}</UserContextStore>
    </ApiContextStore>
  );
};
