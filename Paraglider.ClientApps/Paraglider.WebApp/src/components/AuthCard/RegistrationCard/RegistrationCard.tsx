import { Formik } from "formik";
import { useContext, useState } from "react";

import { ApiContext } from "../../../contexts";
import { RegisterUserRequest } from "../../../typings/server";
import { EmailCard } from "../EmailCard";
import { EmailContainer } from "../EmailCard/EmailCard.styles";
import { AccountInfo } from "./AccountInfo";
import { LoginData } from "./LoginData";
import { RegistrationForm } from "./RegistrationCard.helpers";

enum RegistrationStep {
  LoginData = "loginData",
  AccountInfo = "accountInfo",
  EmailConfirmation = "emailConfirmation",
}

interface RegistrationCardProps {
  onGoToLogin: () => void;
  onClose: () => void;
}

export const RegistrationCard = ({ onGoToLogin }: RegistrationCardProps) => {
  const { accountApi } = useContext(ApiContext);
  const [registrationStep, setRegistrationStep] = useState(RegistrationStep.LoginData);

  const initialValues: RegistrationForm = {
    email: "",
    password: "",
    repeatPassword: "",
    firstName: "",
    surname: "",
    cityId: "",
  };

  const handleGoToAccountInfo = () => setRegistrationStep(RegistrationStep.AccountInfo);

  const handleGoToLoginData = () => setRegistrationStep(RegistrationStep.LoginData);

  const handleGoToEmailConfirmation = () => setRegistrationStep(RegistrationStep.EmailConfirmation);

  const renderRegistrationStep = (email: string) => {
    switch (registrationStep) {
      case RegistrationStep.LoginData:
        return <LoginData onGoBack={onGoToLogin} onGoNext={handleGoToAccountInfo} />;

      case RegistrationStep.AccountInfo:
        return <AccountInfo onGoBack={handleGoToLoginData} />;

      case RegistrationStep.EmailConfirmation:
        return (
          <EmailCard
            title={<>Подтверждение электронной почты</>}
            description={
              <>
                На&nbsp;вашу электронную почту{" "}
                <EmailContainer>&laquo;{email}&raquo;</EmailContainer> было отправлено письмо для
                ее&nbsp;подтверждения
              </>
            }
            onMoveToLogin={onGoToLogin}
          />
        );

      default:
        return null;
    }
  };

  const handleRegister = async (values: RegistrationForm) => {
    const { email, password, firstName, surname, cityId } = values;
    const registerUserRequest: RegisterUserRequest = {
      email,
      password,
      firstName,
      surname,
      cityId,
    };

    try {
      await accountApi.register(registerUserRequest);
      handleGoToEmailConfirmation();
    } catch (e) {
      console.error(e);
    }
  };

  return (
    <Formik initialValues={initialValues} onSubmit={handleRegister}>
      {({ values }) => renderRegistrationStep(values.email)}
    </Formik>
  );
};
