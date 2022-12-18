import { AuthProvider } from "../../../../typings/server";
import { getAuthProviderIconLink, getAuthProviderName } from "./AuthProvider.helpers";
import { AuthProviderBlockRoot, AuthProviderIcon, AuthProviderName } from "./AuthProviders.styles";

interface AuthProviderBlockProps {
  authProvider: AuthProvider;
}

export const AuthProviderBlock = ({ authProvider }: AuthProviderBlockProps) => {
  return (
    <AuthProviderBlockRoot>
      <AuthProviderIcon src={getAuthProviderIconLink(authProvider)} />
      <AuthProviderName>{getAuthProviderName(authProvider)}</AuthProviderName>
    </AuthProviderBlockRoot>
  );
};
