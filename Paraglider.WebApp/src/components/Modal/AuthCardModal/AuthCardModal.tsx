import { SyntheticEvent } from "react";

import { AuthCard } from "../../AuthCard";
import { ModalContainer, ModalContent, ModalScrollContainer } from "./AuthCardModal.styles";

interface AuthCardModalProps {
  onClose: () => void;
}

export const AuthCardModal = ({ onClose }: AuthCardModalProps) => {
  const handleScrollContainerClick = (event: SyntheticEvent) => {
    event.stopPropagation();
  };

  return (
    <ModalContainer>
      <ModalScrollContainer onClick={onClose}>
        <ModalContent onClick={handleScrollContainerClick}>
          <AuthCard />
        </ModalContent>
      </ModalScrollContainer>
    </ModalContainer>
  );
};
