import { PropsWithChildren } from "react";

import { ApiContextStore } from "./ApiContext";
import { AuthModalContextStore } from "./AuthModalContext";
import { UserContextStore } from "./UserContext/UserContextStore";

export const ContextStore = ({ children }: PropsWithChildren) => {
  return (
    <ApiContextStore>
      <UserContextStore>
        <AuthModalContextStore>{children}</AuthModalContextStore>
      </UserContextStore>
    </ApiContextStore>
  );
};
