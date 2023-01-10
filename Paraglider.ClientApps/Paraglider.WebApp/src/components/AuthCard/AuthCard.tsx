import { useState } from "react";

import { LoginCard } from "./LoginCard";
import { RegistrationCard } from "./RegistrationCard";

interface Props {
  onClose: () => void;
}

enum AuthStep {
  Login = "login",
  Registration = "registration",
}

export const AuthCard = ({ onClose }: Props) => {
  const [authStep, setAuthStep] = useState(AuthStep.Login);

  const handleGoToRegistration = () => setAuthStep(AuthStep.Registration);

  const handleGoToLogin = () => setAuthStep(AuthStep.Login);

  switch (authStep) {
    case AuthStep.Login:
      return <LoginCard onGoToRegistration={handleGoToRegistration} onClose={onClose} />;

    case AuthStep.Registration:
      return <RegistrationCard onGoToLogin={handleGoToLogin} />;

    default:
      return null;
  }
};
