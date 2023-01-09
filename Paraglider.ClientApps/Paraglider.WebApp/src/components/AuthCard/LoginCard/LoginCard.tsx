import { Formik } from "formik";
import { useContext } from "react";

import { ApiContext } from "../../../contexts";
import { BasicAuthRequest } from "../../../typings/server";
import { Button } from "../../Buttons";
import { ControlsGroup, Input, Label } from "../../FormControls";
import { CardRoot, TopRightImage } from "../AuthCard.styles";
import { AuthProviders } from "./AuthProviders";
import Circles from "./images/circles.svg";
import { ButtonWrapper, LoginCardTitle } from "./LoginCard.styles";

interface Props {
  onGoToRegistration: () => void;
}

export const LoginCard = ({ onGoToRegistration }: Props) => {
  const { authApi } = useContext(ApiContext);

  const defaultBasicAuthData: BasicAuthRequest = {
    login: "",
    password: "",
  };

  const handleLogin = async (data: BasicAuthRequest) => {
    try {
      await authApi.signIn(data);
    } catch (e) {
      console.error(e);
    }
  };

  return (
    <CardRoot>
      <TopRightImage src={Circles} />
      <LoginCardTitle>Вход</LoginCardTitle>
      <AuthProviders />

      <Formik initialValues={defaultBasicAuthData} onSubmit={handleLogin}>
        {({ values, handleChange, handleSubmit }) => {
          const { login, password } = values;

          return (
            <>
              <ControlsGroup marginBottom={24}>
                <Label>Email</Label>
                <Input name="login" type="email" value={login} onChange={handleChange} />
              </ControlsGroup>
              <ControlsGroup marginBottom={24}>
                <Label>Пароль</Label>
                <Input name="password" type="password" value={password} onChange={handleChange} />
              </ControlsGroup>
              <ButtonWrapper>
                <Button variant="default" type="submit" onClick={handleSubmit}>
                  Войти
                </Button>
                <Button variant="ghost" onClick={onGoToRegistration}>
                  Зарегистрироваться
                </Button>
              </ButtonWrapper>
            </>
          );
        }}
      </Formik>
    </CardRoot>
  );
};
