<template>
  <v-content>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 md4>
          <v-card class="elevation-12">
            <v-toolbar dark color="primary">
              <v-btn v-if="isEditing" icon @click="goBack">
                <v-icon>mdi-arrow-left</v-icon>
              </v-btn>
              <v-toolbar-title>{{ pageTitle }}</v-toolbar-title>
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
              <v-btn color="primary" @click="submitForm">{{
                actionButtonLabel
              }}</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-content>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "ProductForm",
  props: {
    isEditing: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      pageTitle: this.isEditing ? "Editar Produto" : "Adicionar Produto",
      actionButtonLabel: this.isEditing ? "Atualizar" : "Cadastrar",
      product: {
        name: "",
        price: 0,
      },
    };
  },
  methods: {
    async submitForm() {
      if (!this.product.name || this.product.price <= 0) {
        alert("Preencha o nome do produto e um preço válido.");
        return;
      }

      const apiUrl = this.$config.url_base;

      const endpoint = this.isEditing ? `/${this.$route.params.id}` : "";
      const method = this.isEditing ? "PUT" : "POST";

      const apiData = {
        name: this.product.name,
        value: this.product.price,
      };

      try {
        const response = await fetch(`${apiUrl}/product${endpoint}`, {
          method,
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(apiData),
        });

        if (response.ok) {
          alert(
            `Produto ${
              this.isEditing ? "atualizado" : "cadastrado"
            } com sucesso!`
          );

          if (!this.isEditing) {
            this.product.name = "";
            this.product.price = 0;
          }
        } else {
          const errorData = await response.json();
          alert(`Erro: ${errorData.ErrorMessage.Message}`);
        }
      } catch (error) {
        alert(
          "Erro ao realizar a operação. Por favor, tente novamente mais tarde."
        );
      }
    },
    async fetchProduct() {
      if (!this.isEditing) return;

      try {
        const response = await fetch(
          `${this.$config.url_base}/product/${this.$route.params.id}`
        );

        const data = await response.json();

        if (data.success) {
          this.product.name = data.result.name;
          this.product.price = data.result.value;
        } else {
          console.error("Erro ao buscar produto:", data.ErrorMessage.Message);
        }
      } catch (error: any) {
        console.error("Erro ao buscar produto:", error.message);
      }
    },
    goBack() {
      this.$router.go(-1);
    },
  },
  mounted() {
    this.fetchProduct();
  },
});
</script>
