describe("Ações de registro", () => {
  beforeEach(() => {
    cy.request({
      method: "POST",
      url: "http://localhost:5167/auth/login",
      body: {
        email: "admin@example.com",
        senha: "admin123",
      },
    }).then((response) => {
      localStorage.setItem("token", response.body.token);
      localStorage.setItem(
        "tokenExpiration",
        (Date.now() + 3600000).toString(),
      );
    });
  });
  it("deve criar um registro", () => {
    cy.visit("/registros");

    cy.contains("Criar Registro").click();

    cy.get("select").select("Contrato");
    cy.get("input").eq(0).type("João Silva");
    cy.get("input").eq(1).type("42591651000143");
    cy.get('input[type="date"]').type("2026-06-21");
    cy.get("textarea").type("Observação teste");

    cy.contains("Salvar").click();

    cy.on("window:confirm", () => true);

    cy.url().should("include", "/registros");

    cy.contains("Criar Registro").click();

    cy.get("select").select("Contrato");
    cy.get("input").eq(0).type("João Silva");
    cy.get("input").eq(1).type("42591651000143");
    cy.get('input[type="date"]').type("2026-06-21");
    cy.get("textarea").type("Observação teste");

    cy.contains("Salvar").click();

    cy.on("window:confirm", () => true);

    cy.url().should("include", "/registros");
  });

  it("deve editar um registro", () => {
    cy.visit("/registros");

    cy.get(".bi-pencil-fill").first().click();

    cy.get("input").first().clear().type("Nome Alterado");

    cy.contains("Salvar").click();

    cy.url().should("include", "/registros");
  });

  it("deve registrar um pedido pendente", () => {
    cy.visit("/registros");

    cy.window().then((win) => {
      cy.stub(win, "confirm").returns(true);
    });

    cy.contains("Registrar").first().click();
  });

  it("deve devolver um pedido pendente", () => {
    cy.visit("/registros");

    cy.window().then((win) => {
      cy.stub(win, "confirm").returns(true);
    });

    cy.contains("Devolver").first().click();
  });

  it("deve exibir erro de CPF/CNPJ inválido", () => {
    cy.visit("/registros");

    cy.contains("Criar Registro").click();

    cy.get("select").select("Contrato");
    cy.get("input").eq(0).type("João Silva");
    cy.get("input").eq(1).type("12345678901");
    cy.get('input[type="date"]').type("2026-06-21");
    cy.get("textarea").type("Observação teste");

    cy.contains("Salvar").click();

    cy.contains("CPF/CNPJ inválido.")

    cy.url().should("eq", Cypress.config().baseUrl + "/registros/novo");
  });

    it("deve exibir erro de Apresentante deve ser preenchido", () => {
    cy.visit("/registros");

    cy.contains("Criar Registro").click();

    cy.get("select").select("Contrato");
    cy.get("input").eq(1).type("42591651000143");
    cy.get('input[type="date"]').type("2026-06-21");
    cy.get("textarea").type("Observação teste");

    cy.contains("Salvar").click();

    cy.contains("Nome apresentante deve ser preenchido.")

    cy.url().should("eq", Cypress.config().baseUrl + "/registros/novo");
  });

    it("deve exibir erro de Data futura", () => {
    cy.visit("/registros");

    cy.contains("Criar Registro").click();

    cy.get("select").select("Contrato");
    cy.get("input").eq(0).type("João Silva");
    cy.get("input").eq(1).type("42591651000143");
    cy.get('input[type="date"]').type("2777-06-21");
    cy.get("textarea").type("Observação teste");

    cy.contains("Salvar").click();

    cy.contains("Data de entrada não pode ser futura.")

    cy.url().should("eq", Cypress.config().baseUrl + "/registros/novo");
  });
});
