<template>
  <v-container fluid fill-height>
    <v-layout align-center justify-center>
      <v-flex xs12>
        <v-card class="elevation-12">
          <v-card-title>{{ title }}</v-card-title>
          <v-data-table
            :items="formattedItems"
            :headers="headers"
            :items-per-page="5"
          >
            <template v-slot:item.edit="{ item }">
              <v-btn class="blue" icon>
                <NuxtLink :to="`/${itemType}/${item.id}`" class="white--text">
                  <v-icon>mdi-pencil</v-icon>
                </NuxtLink>
              </v-btn>
            </template>
            <template v-slot:item.delete="{ item }">
              <v-btn class="red" icon @click="deleteItem(item.id)">
                <v-icon>mdi-delete</v-icon>
              </v-btn>
            </template>
          </v-data-table>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Product } from "~/interfaces/product";

export default defineComponent({
  name: "TableList",
  props: {
    title: String,
    itemType: String,
  },
  data() {
    return {
      items: [] as any[],
      headers: [
        { text: "Nome", value: "name" },
        { text: "Valor", value: "valueFormatted" },
        { text: "Editar", value: "edit", sortable: false },
        { text: "Excluir", value: "delete", sortable: false },
      ],
    };
  },
  computed: {
    formattedItems(): any[] {
      if (this.itemType == "order") {
        return this.items.map((order: any) => ({
          ...order,
          product: {
            ...order.product,
            valueFormatted: this.formatCurrency(order.product.value),
          },
          valorFormatted: this.formatCurrency(order.valor),
          createdAt: this.formatDate(order.createdAt),
        }));
      }

      return this.items.map((item: any) => ({
        ...item,
        valueFormatted: this.formatCurrency(item.value || item.valor),
      }));
    },
  },
  mounted() {
    this.getAllItems();

    if (this.itemType == "order") {
      this.headers = [
        { text: "Produto", value: "product.name" },
        { text: "Quantidade", value: "quantity" },
        { text: "Valor Do produto", value: "product.valueFormatted" },
        { text: "Valor Total", value: "valorFormatted" },
        { text: "Data do pedido", value: "createdAt" },
        { text: "Editar", value: "edit", sortable: false },
        { text: "Excluir", value: "delete", sortable: false },
      ];
    }
  },
  methods: {
    async getAllItems() {
      try {
        const response = await fetch(
          `${this.$config.url_base}/${this.itemType}`
        );

        const data = await response.json();

        if (data.success) {
          this.items = data.result;
        } else {
          alert(`Erro: ${data.ErrorMessage.Message}`);
        }
      } catch (error) {
        alert(
          `Erro ao buscar ${this.itemType}s. Por favor, tente novamente mais tarde.`
        );
      }
    },

    formatCurrency(value: number) {
      return value.toLocaleString("pt-BR", {
        style: "currency",
        currency: "BRL",
      });
    },

    formatDate(dateString: string) {
      const date = new Date(dateString);
      const day = String(date.getDate()).padStart(2, "0");
      const month = String(date.getMonth() + 1).padStart(2, "0");
      const year = date.getFullYear();
      return `${day}-${month}-${year}`;
    },

    async deleteItem(id: number) {
      if (
        confirm(
          `Tem certeza que deseja excluir este ${this.itemType}? ${
            this.itemType === "product"
              ? "Isso tambem excuirar todos pedidos relacionado a ele"
              : ""
          } `
        )
      ) {
        try {
          const response = await fetch(
            `${this.$config.url_base}/${this.itemType}/${id}`,
            {
              method: "DELETE",
            }
          );

          if (response.ok) {
            alert(
              `${
                this.itemType === "order" ? "Pedido" : "Produto"
              } excluído com sucesso!`
            );
            await this.getAllItems();
          } else {
            const data = await response.json();
            alert(`Erro ao excluir ${this.itemType}: ${data.message}`);
          }
        } catch (error) {
          alert(
            `Erro ao excluir ${this.itemType}. Por favor, tente novamente mais tarde.`
          );
        }
      }
    },
  },
});
</script>
