import { useContext } from "react";

import { ApiContext } from "../../../../contexts";
import { AuthProvider } from "../../../../typings/server";
import { getAuthProviderIconLink, getAuthProviderName } from "./AuthProvider.helpers";
import { AuthProviderBlockRoot, AuthProviderIcon, AuthProviderName } from "./AuthProviders.styles";

interface AuthProviderBlockProps {
  authProvider: AuthProvider;
}

export const AuthProviderBlock = ({ authProvider }: AuthProviderBlockProps) => {
  const { authApi } = useContext(ApiContext);

  return (
    <AuthProviderBlockRoot href={authApi.getExternalAuthUrl(authProvider)}>
      <AuthProviderIcon src={getAuthProviderIconLink(authProvider)} />
      <AuthProviderName>{getAuthProviderName(authProvider)}</AuthProviderName>
    </AuthProviderBlockRoot>
  );
};
