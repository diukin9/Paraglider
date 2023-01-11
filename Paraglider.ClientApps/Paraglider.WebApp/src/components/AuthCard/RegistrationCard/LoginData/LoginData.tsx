import { useFormikContext } from "formik";

import { Button } from "../../../Buttons";
import { BackButton } from "../../../Buttons/BackButton";
import { ControlsGroup, Input, Label } from "../../../FormControls";
import { CardBackButtonWrapper, CardRoot, CardTitle, TopRightImage } from "../../AuthCard.styles";
import { GirlIcon } from "../GirlIcon";
import { RegistrationForm } from "../RegistrationCard.helpers";
import Circles from "./images/circles.svg";
import { ButtonWrapper } from "./LoginData.styles";

interface LoginDataProps {
  onGoBack: () => void;
  onGoNext: () => void;
}

export const LoginData = ({ onGoBack, onGoNext }: LoginDataProps) => {
  const { values, handleChange } = useFormikContext<RegistrationForm>();

  return (
    <CardRoot>
      <TopRightImage src={Circles} />

      <CardBackButtonWrapper>
        <BackButton onClick={onGoBack}>Вход</BackButton>
      </CardBackButtonWrapper>

      <CardTitle>Регистрация</CardTitle>

      <GirlIcon />

      <ControlsGroup marginBottom={24}>
        <Label>Email</Label>
        <Input name="email" type="email" value={values.email} onChange={handleChange} />
      </ControlsGroup>

      <ControlsGroup marginBottom={24}>
        <Label>Пароль</Label>
        <Input name="password" type="password" value={values.password} onChange={handleChange} />
      </ControlsGroup>

      <ControlsGroup>
        <Label>Повторите пароль</Label>
        <Input
          name="repeatPassword"
          type="password"
          value={values.repeatPassword}
          onChange={handleChange}
        />
      </ControlsGroup>

      <ButtonWrapper>
        <Button variant="default" onClick={onGoNext}>
          Продолжить
        </Button>
      </ButtonWrapper>
    </CardRoot>
  );
};
