import { AuthProvider } from "../../../../typings/server";
import { AuthProviderBlock } from "./AuthProviderBlock";
import {
  AuthProvidersContainer,
  AuthProvidersRoot,
  AuthProvidersTitle,
} from "./AuthProviders.styles";

export const AuthProviders = () => {
  const authProviders = Object.values(AuthProvider);

  return (
    <AuthProvidersRoot>
      <AuthProvidersTitle>Войти с&nbsp;помощью</AuthProvidersTitle>
      <AuthProvidersContainer>
        {authProviders.map((authProvider) => (
          <AuthProviderBlock authProvider={authProvider} key={authProvider} />
        ))}
      </AuthProvidersContainer>
    </AuthProvidersRoot>
  );
};
