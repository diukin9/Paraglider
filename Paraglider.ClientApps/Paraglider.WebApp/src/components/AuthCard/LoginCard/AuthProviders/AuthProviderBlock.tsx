import { AuthProvider } from "../../../../typings/server";
import { getAuthProviderIconLink, getAuthProviderName } from "./AuthProvider.helpers";
import { AuthProviderBlockRoot, AuthProviderIcon, AuthProviderName } from "./AuthProviders.styles";

interface AuthProviderBlockProps {
  authProvider: AuthProvider;
}

export const AuthProviderBlock = ({ authProvider }: AuthProviderBlockProps) => {
  const getAuthProviderUrl = () => {
    const url = new URL("api/v1/auth/web/" + authProvider, import.meta.env.VITE_API_PROXY_URL);
    url.searchParams.set("callback", window.location.href);
    return url.toString();
  };

  return (
    <AuthProviderBlockRoot href={getAuthProviderUrl()}>
      <AuthProviderIcon src={getAuthProviderIconLink(authProvider)} />
      <AuthProviderName>{getAuthProviderName(authProvider)}</AuthProviderName>
    </AuthProviderBlockRoot>
  );
};
