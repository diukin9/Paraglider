import { Formik } from "formik";
import { useState } from "react";

import { AccountInfo } from "./AccountInfo";
import { LoginData } from "./LoginData";
import { RegistrationFrom } from "./RegistrationCard.helpers";

enum RegistrationStep {
  LoginData = "loginData",
  AccountInfo = "accountInfo",
}

interface RegistrationCardProps {
  onGoToLogin: () => void;
}

export const RegistrationCard = ({ onGoToLogin }: RegistrationCardProps) => {
  const [registrationStep, setRegistrationStep] = useState(RegistrationStep.LoginData);

  const initialValues: RegistrationFrom = {
    email: "",
    password: "",
    repeatPassword: "",
    firstName: "",
    surname: "",
    cityId: "",
  };

  const handleGoToAccountInfo = () => setRegistrationStep(RegistrationStep.AccountInfo);

  const handleGoToLoginData = () => setRegistrationStep(RegistrationStep.LoginData);

  const renderRegistrationStep = () => {
    switch (registrationStep) {
      case RegistrationStep.LoginData:
        return <LoginData onGoBack={onGoToLogin} onGoNext={handleGoToAccountInfo} />;

      case RegistrationStep.AccountInfo:
        return <AccountInfo onGoBack={handleGoToLoginData} />;

      default:
        return null;
    }
  };

  const handleRegister = () => {
    return;
  };

  return (
    <Formik initialValues={initialValues} onSubmit={handleRegister}>
      {renderRegistrationStep()}
    </Formik>
  );
};
