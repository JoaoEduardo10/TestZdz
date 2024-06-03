<template>
  <v-content>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 md4>
          <v-card class="elevation-12">
            <v-toolbar dark color="primary">
              <v-btn icon @click="$router.push('/listProduct')">
                <v-icon>mdi-arrow-left</v-icon>
              </v-btn>
              <v-toolbar-title>Adicionar um novo produto</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <v-form ref="form">
                <v-text-field
                  v-model="product.name"
                  id="name"
                  name="name"
                  label="Nome do Produto"
                  type="text"
                ></v-text-field>
                <v-text-field
                  v-model="product.price"
                  id="price"
                  name="price"
                  label="Valor"
                  type="number"
                ></v-text-field>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" @click="updateProduct">atualizar</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-content>
</template>

<script>
export default {
  name: "ProductEditPage",
  data() {
    return {
      params: {
        id: 0,
      },
      product: {
        name: "",
        price: 0,
      },
    };
  },
  mounted() {
    this.fetchProduct();
  },
  methods: {
    async fetchProduct() {
      try {
        this.params.id = parseInt($nuxt.$route.params.id);

        const response = await fetch(
          `http://localhost:5042/api/v1/product/${this.params.id}`
        );

        const data = await response.json();

        if (data.success) {
          this.product.name = data.result.name;
          this.product.price = data.result.value;
        } else {
          console.error("Erro ao buscar pedido:", data.ErrorMessage.Message);
        }
      } catch (error) {
        console.error("Erro ao buscar pedido:", error.message);
      }
    },
    async updateProduct() {
      if (!this.product.name || this.product.price <= 0) {
        alert("Adicione o nome do produto e preço inválido.");
        return;
      }

      const productData = {
        name: this.product.name,
        value: this.product.price,
      };

      try {
        const response = await fetch(
          `http://localhost:5042/api/v1/product/${this.params.id}`,
          {
            method: "PUT",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(productData),
          }
        );

        if (response.ok) {
          alert("Produto atualizado com sucesso!");
        } else {
          const data = await response.json();
          alert(`Erro ao atualizar o produto: ${data.ErrorMessage.Message}`);
        }
      } catch (error) {
        console.error("Erro ao atualizar o produto:", error.message);
        alert(
          "Erro ao atualizar o produto. Por favor, tente novamente mais tarde."
        );
      }
    },
  },
};
</script>
