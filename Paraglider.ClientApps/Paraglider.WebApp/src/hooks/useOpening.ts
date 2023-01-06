import { useCallback, useState } from "react";

interface useOpeningObject {
  isOpen: boolean;
  open: () => void;
  close: () => void;
  toggle: () => void;
}

export const useOpening = (defaultIsOpen = false): useOpeningObject => {
  const [isOpen, setIsOpen] = useState(defaultIsOpen);

  const open = useCallback(() => setIsOpen(true), []);

  const close = useCallback(() => setIsOpen(false), []);

  const toggle = useCallback(() => setIsOpen((isOpen) => !isOpen), []);

  return { isOpen, open, close, toggle };
};
