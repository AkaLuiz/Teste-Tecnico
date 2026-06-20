interface Props {
  value: string;
  onChange: (value: string) => void;
  className?: string;
}

export default function CpfCnpjInput({
  value,
  onChange,
  className = "form-control",
}: Props) {

  function formatar(valor: string) {

    const numeros = valor.replace(/\D/g, "");

    if (numeros.length <= 11) {
      return numeros
        .replace(/(\d{3})(\d)/, "$1.$2")
        .replace(/(\d{3})(\d)/, "$1.$2")
        .replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    }

    return numeros
      .replace(/^(\d{2})(\d)/, "$1.$2")
      .replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3")
      .replace(/\.(\d{3})(\d)/, ".$1/$2")
      .replace(/(\d{4})(\d)/, "$1-$2");
  }

  function handleChange(
    e: React.ChangeEvent<HTMLInputElement>
  ) {
    onChange(formatar(e.target.value));
  }

  return (
    <input
      type="text"
      className={className}
      value={value}
      onChange={handleChange}
    />
  );
}